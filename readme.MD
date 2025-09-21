# Listify

app de lista de compras hecha con .NET MAUI y .NET 8. usa MVVM, SQLite y navegacion con Shell.

## contenido rapido

- agregar, editar, eliminar articulos
- marcar como comprado (texto tachado)
- busqueda por nombre
- datos guardados en SQLite
- arquitectura MVVM con CommunityToolkit.Mvvm
- inyeccion de dependencias (DI)

---

## requisitos

- .NET 8 SDK
- workloads de maui instalados
    dotnet workload install maui
- android sdk / xcode segun plataforma de destino

## ejecutar

    # restaurar y compilar
    dotnet restore
    dotnet build

    # correr en cada plataforma (elige una)
    dotnet build -t:Run -f net8.0-android
    dotnet build -t:Run -f net8.0-windows   # solo en windows
    dotnet build -t:Run -f net8.0-ios       # requiere xcode
    dotnet build -t:Run -f net8.0-maccatalyst

---

## arquitectura (vista general)

    +-----------+       binding        +-----------------------+       CRUD        +------------------+
    |  Vistas   | <------------------> |       ViewModels      | <--------------> |     Servicios     |
    |  XAML     |  props y comandos    | logica de pantalla    | usa SQLite       | BaseDeDatosServ.  |
    +-----------+                      +-----------------------+                   +------------------+
          |                                                                                        |
          | recursos globales (converter, estilos)                                                 v
          +-----------------------------------------> App.xaml                            +------------------+
                                                                                          |     SQLite      |
                                                                                          |  tabla:articulos|
                                                                                          +------------------+

### flujo basico

1. App crea AppShell
2. AppShell muestra PrincipalPagina
3. PrincipalVistaModelo carga articulos desde BaseDeDatosServicio
4. el usuario puede buscar, agregar, editar, eliminar, marcar comprado
5. cambios se guardan en SQLite y la UI se actualiza sola (INotifyPropertyChanged)

---

## estructura de carpetas (simplificada)

    Listify/
    ├─ App.xaml
    ├─ App.xaml.cs
    ├─ AppShell.xaml
    ├─ AppShell.xaml.cs
    ├─ MauiProgram.cs
    ├─ Modelos/
    │  └─ Articulo.cs
    ├─ ViewModels/
    │  ├─ PrincipalVistaModelo.cs
    │  └─ EditarArticuloVistaModelo.cs
    ├─ Vistas/
    │  ├─ PrincipalPagina.xaml
    │  ├─ PrincipalPagina.xaml.cs
    │  ├─ EditarArticuloPagina.xaml
    │  └─ EditarArticuloPagina.xaml.cs
    ├─ Recursos/
    │  └─ BoolToTextDecorationConverter.cs
    └─ Resources/Styles/
       ├─ Colors.xaml
       └─ Styles.xaml

---

## modelos

Articulo.cs

- Id (int, PK, autoincrement)
- Nombre (string, not null)
- Cantidad (int, default 1)
- Comprado (bool)
- implementa INotifyPropertyChanged para que la UI se refresque

tabla en SQLite: articulos

---

## vistas y viewmodels

| vista                  | viewmodel                      | que hace                                                                 |
|------------------------|--------------------------------|---------------------------------------------------------------------------|
| PrincipalPagina        | PrincipalVistaModelo           | lista, busqueda, eliminar, alternar comprado, navegar a edicion          |
| EditarArticuloPagina   | EditarArticuloVistaModelo      | crear o editar un articulo, validar y guardar                            |

bindings claves:

    # PrincipalPagina
    <CollectionView ItemsSource="{Binding Articulos}" />
    <CheckBox IsChecked="{Binding Comprado, Mode=TwoWay}" />
    <Label Text="{Binding Nombre}"
           TextDecorations="{Binding Comprado, Converter={StaticResource BoolToTextDecorationConverter}}" />
    <SearchBar Text="{Binding TextoBusqueda, Mode=TwoWay}" />

    # EditarArticuloPagina
    <Entry Text="{Binding Articulo.Nombre}" />
    <Entry Text="{Binding Articulo.Cantidad}" Keyboard="Numeric" />
    <Button Text="Guardar" Command="{Binding GuardarCommand}" />
    <Button Text="Cancelar" Command="{Binding CancelarCommand}" />

eventos de code-behind usados:

- SearchBar_TextChanged -> ejecuta BuscarArticulosCommand
- CheckBox_CheckedChanged -> llama EstablecerCompradoAsync(articulo, valor)

---

## navegacion (Shell)

rutas

- PrincipalPagina (en AppShell.xaml)
- EditarArticuloPagina registrada en AppShell.xaml.cs

    // AppShell.xaml.cs
    Routing.RegisterRoute("EditarArticuloPagina", typeof(EditarArticuloPagina));

navegar:

    // abrir pantalla de edicion (nuevo articulo)
    await Shell.Current.GoToAsync("EditarArticuloPagina");

    // abrir pantalla de edicion (editar existente)
    await Shell.Current.GoToAsync("EditarArticuloPagina", new Dictionary<string, object>
    {
        { "Articulo", articuloSeleccionado }
    });

    // volver atras
    await Shell.Current.GoToAsync("..");

EditarArticuloVistaModelo recibe el parametro con:
[QueryProperty(nameof(Articulo), "Articulo")].

---

## recursos y estilos

App.xaml registra:

- BoolToTextDecorationConverter (true -> texto tachado, false -> normal)
- colores y estilos globales: Resources/Styles/Colors.xaml, Resources/Styles/Styles.xaml

uso del converter:

    TextDecorations="{Binding Comprado, Converter={StaticResource BoolToTextDecorationConverter}}"

---

## inyeccion de dependencias (DI)

registro en MauiProgram.cs:

    builder.Services.AddSingleton<BaseDeDatosServicio>();       // servicio de BD (una instancia)
    builder.Services.AddSingleton<PrincipalVistaModelo>();      // VM principal (conserva estado)
    builder.Services.AddTransient<EditarArticuloVistaModelo>(); // VM de edicion (instancia por visita)

consumo en paginas (service locator simple):

    BindingContext = ServicioLocalizador.Obtener<PrincipalVistaModelo>();
    BindingContext = ServicioLocalizador.Obtener<EditarArticuloVistaModelo>();

---

## servicio de base de datos

BaseDeDatosServicio expone metodos usados por los viewmodels:

- ObtenerArticulosAsync()
- BuscarArticulosAsync(texto)
- AgregarArticuloAsync(articulo)
- ActualizarArticuloAsync(articulo)
- EliminarArticuloAsync(articulo)

usa la libreria SQLite con atributos [Table], [PrimaryKey], etc.

---

## validaciones y errores

- al guardar: valida que Nombre no este vacio
- al eliminar: pide confirmacion
- al alternar Comprado: si falla la BD, revierte el cambio y muestra alerta

---

## consejos de calidad

- evitar trabajo pesado en el hilo de UI (usa metodos async)
- notificar cambios solo cuando el valor realmente cambia
- usar ObservableCollection para listas que cambian
- separar logica de UI en viewmodels (MVVM)
- seleccionar correctamente el ciclo de vida en DI:
  - Singleton para servicios compartidos
  - Transient para viewmodels de paginas que cambian seguido

---

## problemas comunes

- no navega a EditarArticuloPagina  
  asegurar: Routing.RegisterRoute("EditarArticuloPagina", typeof(EditarArticuloPagina));

- bindings no actualizan  
  verificar INotifyPropertyChanged y OnPropertyChanged en Articulo. revisar nombres de propiedad.

- no se encuentra el converter  
  confirmar que BoolToTextDecorationConverter esta registrado en App.xaml y el xmlns es correcto en XAML.

- fallos en android/iOS  
  revisar permisos y versiones de sdk. limpiar y recompilar:
      dotnet clean
      dotnet build

---

## roadmap corto

- ordenar por comprado / pendiente
- editar cantidad desde la lista
- persistir preferencias (tema claro/oscuro)
- soporte de arrastrar para reordenar

---

## licencia

este proyecto puede usarse con fines educativos y personales. agrega una licencia si lo publicas o a fines practicos se rige por el MIT.

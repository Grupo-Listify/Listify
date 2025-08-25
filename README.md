# 🛒 Listify (Lista de Compras)

Aplicación móvil y de escritorio desarrollada en **.NET MAUI** con arquitectura **MVVM** y persistencia en **SQLite**.  
Permite crear, organizar y gestionar listas de compras de manera sencilla.

---

## 🚀 Características principales

- CRUD de artículos (agregar, editar, eliminar).
- Marcar artículos como **comprados** o **pendientes**.
- Búsqueda y filtrado por categorías.
- Soporte para cantidades, notas, precio y tienda.
- Persistencia local con **SQLite**.
- Arquitectura **MVVM** para mantener separación entre UI y lógica.
- Multiplataforma: Android, iOS, Windows, Mac.

---

## 🗂️ Estructura del proyecto

```
lystify/
├─ src/
│ └─ Lystify/
│ ├─ App.xaml / App.xaml.cs
│ ├─ MauiProgram.cs
│ ├─ Models/
│ │ ├─ Item.cs
│ │ └─ Category.cs
│ ├─ Data/
│ │ ├─ AppDbContext.cs
│ │ └─ migrations.sql
│ ├─ Services/
│ │ ├─ IItemRepository.cs
│ │ ├─ ICategoryRepository.cs
│ │ └─ Implementaciones...
│ ├─ ViewModels/
│ │ ├─ ItemsViewModel.cs
│ │ └─ CategoriesViewModel.cs
│ └─ Views/
│ ├─ ItemsPage.xaml
│ ├─ ItemDetailPage.xaml
│ └─ CategoriesPage.xaml
├─ tests/
│ └─ Lystify.Tests/
├─ docs/
│ └─ er-diagram.pdf
├─ .gitignore
├─ README.md
├─ LICENSE
└─ CONTRIBUTING.md
```

---

## 🛠️ Tecnologías utilizadas

- [.NET 8.0 LTS](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/what-is-maui)  
- [sqlite-net-pcl](https://www.nuget.org/packages/sqlite-net-pcl)  
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/introduction)  

---

## ⚙️ Instalación y ejecución

1. Clona el repositorio:

   ```bash
   git clone https://github.com/kevindoblea/Listify.git
   cd shopping-list-maui/src/Listify
   ```

2. Restaura los paquetes NuGet:

   ```bash
   dotnet restore
   ```

3. Compila el proyecto:

   ```bash
   dotnet build
   ```

4. Ejecuta la app en Windows:

   ```bash
   dotnet run -f net8.0-windows10.0.19041.0
   ```

   O en Android/iOS desde Visual Studio seleccionando el emulador/dispositivo.

---

## 🗃️ Base de datos

- Motor: **SQLite**.  
- Script de creación: [`migrations.sql`](src/Listify/Data/migrations.sql).  
- Esquema ER: ver [`docs/er-diagram.pdf`](docs/er-diagram.pdf).  

---

## 📸 Capturas de pantalla (próximamente)

*(Aquí van imágenes del app en uso una vez implementadas las vistas).*

---

## 📌 Planificación con Scrum

### Roles
- **Product Owner (PO):** define prioridades y criterios de aceptación.  
- **Scrum Master:** facilita las ceremonias y ayuda a remover bloqueos.  
- **Equipo de desarrollo:** implementación de funcionalidad, pruebas y documentación.  

### Ceremonias
- **Sprint Planning:** planificación de historias a desarrollar.  
- **Daily Scrum:** reunión corta para revisar avances (puede ser por chat).  
- **Sprint Review:** demostración del incremento terminado.  
- **Sprint Retrospective:** mejora continua del equipo.  

### Sprints sugeridos

#### Sprint 1 – Fundamentos
- Configuración inicial del proyecto MAUI.  
- Creación del esquema de base de datos SQLite.  
- Implementación de CRUD básico para **Item** y **Category**.  
- Entregable: app corriendo con almacenamiento local.  

#### Sprint 2 – Funcionalidad principal
- Implementación de búsqueda y filtrado.  
- Marcar artículos como comprados.  
- Mejoras en la interfaz (UI + UX).  
- Validaciones de datos.  
- Entregable: lista de compras funcional y usable.  

#### Sprint 3 – Calidad y demo final
- Pruebas unitarias y refinamiento de código.  
- Optimización de rendimiento en listas.  
- Documentación (README, diagrama ER, guía de usuario).  
- Preparación de demo y presentación.  
- Entregable: versión **v1.0.0** lista para entrega.  

### Product Backlog (ejemplos)
- Crear, editar y eliminar artículos.  
- Marcar artículos como comprados/pendientes.  
- Filtrar por categoría.  
- Buscar por nombre o notas.  
- Gestionar categorías.  
- Mejorar experiencia visual y accesibilidad.  
- Documentar proyecto.  

---

## 👥 Contribución

1. Haz un fork del repositorio.  
2. Crea una rama de feature:  
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Haz commit de tus cambios:  
   ```bash
   git commit -m "feat: agrega nueva funcionalidad"
   ```
4. Sube la rama:  
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Abre un **Pull Request**.

---

## 📄 Licencia

Este proyecto está bajo licencia **MIT**.  
Ver el archivo [LICENSE](LICENSE) para más detalles.

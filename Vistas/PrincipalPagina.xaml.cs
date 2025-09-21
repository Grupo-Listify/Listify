using Microsoft.Maui.Controls;
using Listify.ViewModels;
using Listify.Modelos;
using Listify.Servicios;

namespace Listify.Vistas;

/// <summary>
/// Pagina principal de la app.
/// Muestra la lista de articulos y permite buscar, marcar como comprado y eliminar.
/// El BindingContext se resuelve desde el contenedor de servicios (ServicioLocalizador).
/// </summary>
public partial class PrincipalPagina : ContentPage
{
    /// <summary>
    /// Atajo para obtener el ViewModel tipado desde el BindingContext.
    /// El operador ! indica al compilador que aqui no sera nulo luego de inicializar el contexto.
    /// </summary>
    private PrincipalVistaModelo VistaModelo => BindingContext as PrincipalVistaModelo;

    /// <summary>
    /// Constructor por defecto.
    /// Inicializa los componentes y asigna el ViewModel de la pagina usando DI.
    /// </summary>
    public PrincipalPagina()
    {
        InitializeComponent();
        // obtiene una instancia del viewmodel desde el contenedor y la asigna al contexto de enlace
        BindingContext = ServicioLocalizador.Obtener<PrincipalVistaModelo>();
    }

    /// <summary>
    /// Maneja los cambios de texto en la barra de busqueda.
    /// Llama al comando de busqueda del viewmodel con el nuevo texto.
    /// </summary>
    /// <param name="sender">SearchBar que dispara el evento.</param>
    /// <param name="e">Valores de texto anterior y nuevo.</param>
    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Ejecuta la busqueda de forma asincrona (no bloquea la UI)
        await VistaModelo!.BuscarArticulosCommand.ExecuteAsync(e.NewTextValue);
    }

    /// <summary>
    /// Maneja el cambio del CheckBox que marca un articulo como comprado o pendiente.
    /// Si el valor ya coincide, no hace nada. Si cambia, actualiza en la base de datos.
    /// </summary>
    /// <param name="sender">CheckBox que cambio.</param>
    /// <param name="e">Nuevo valor del CheckBox.</param>
    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // verifica que el sender sea un CheckBox y su contexto sea un Articulo
        if (sender is CheckBox checkBox && checkBox.BindingContext is Articulo articulo)
        {
            // si el estado del modelo ya es el mismo que el del CheckBox, salir
            if (articulo.Comprado == e.Value)
                return;

            // obtiene el viewmodel del contexto y actualiza el estado
            if (BindingContext is PrincipalVistaModelo vm)
            {
                await vm.EstablecerCompradoAsync(articulo, e.Value);
            }
        }
    }
}

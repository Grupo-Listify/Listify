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
    /// </summary>
    private PrincipalVistaModelo VistaModelo => BindingContext as PrincipalVistaModelo;

    /// <summary>
    /// Constructor por defecto.
    /// Inicializa los componentes y asigna el ViewModel de la pagina usando DI.
    /// </summary>
    public PrincipalPagina()
    {
        InitializeComponent();
        BindingContext = ServicioLocalizador.Obtener<PrincipalVistaModelo>();
    }

    /// <summary>
    /// Se ejecuta cada vez que la pagina vuelve a ser visible.
    /// Recarga la lista desde BD para reflejar articulos nuevos o editados.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PrincipalVistaModelo vm)
        {
            await vm.CargarArticulosCommand.ExecuteAsync(null);
        }
    }

    /// <summary>
    /// Maneja los cambios de texto en la barra de busqueda.
    /// Llama al comando de busqueda del viewmodel con el nuevo texto.
    /// </summary>
    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        await VistaModelo!.BuscarArticulosCommand.ExecuteAsync(e.NewTextValue);
    }

    /// <summary>
    /// Marca un articulo como comprado/pendiente y PERSISTE SIEMPRE el cambio.
    /// Nota: con Mode=TwoWay el binding ya actualizo Articulo.Comprado antes de este evento.
    /// </summary>
    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Articulo articulo)
        {
            // Persistimos siempre el nuevo valor (e.Value).
            // Evitamos el return cuando articulo.Comprado == e.Value,
            // porque eso impedia llamar a la BD.
            if (BindingContext is PrincipalVistaModelo vm)
            {
                await vm.EstablecerCompradoAsync(articulo, e.Value);
            }
        }
    }
}

using Listify.ViewModels;
using Listify.Modelos;
using Listify.Servicios;

namespace Listify.Vistas;

public partial class PrincipalPagina : ContentPage
{
    private PrincipalVistaModelo VistaModelo => BindingContext as PrincipalVistaModelo;

    public PrincipalPagina()
    {
        InitializeComponent();
        BindingContext = ServicioLocalizador.Obtener<PrincipalVistaModelo>();
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        await VistaModelo!.BuscarArticulosCommand.ExecuteAsync(e.NewTextValue);
    }

    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Articulo articulo)
        {
            if (articulo.Comprado == e.Value)
                return;

            if (BindingContext is PrincipalVistaModelo vm)
            {
                await vm.EstablecerCompradoAsync(articulo, e.Value);
            }
        }
    }
} 

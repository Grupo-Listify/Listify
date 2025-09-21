using Listify.ViewModels;
using Listify.Modelos;
using Listify.Servicios;

namespace Listify.Vistas
{
    // Esta clase es el code-behind de la página principal (PrincipalPagina).
    // Aquí se conecta la vista XAML con el ViewModel.
    // También se manejan algunos eventos de la UI como búsqueda y checkbox.
    public partial class PrincipalPagina : ContentPage
    {
        // Acceso rápido al ViewModel (PrincipalVistaModelo) que está como BindingContext.
        private PrincipalVistaModelo VistaModelo => BindingContext as PrincipalVistaModelo;

        public PrincipalPagina()
        {
            InitializeComponent();

            // Asignamos el BindingContext usando el ServicioLocalizador.
            // Así obtenemos el ViewModel registrado y lo ligamos a la vista.
            BindingContext = ServicioLocalizador.Obtener<PrincipalVistaModelo>();
        }

        // Evento que se dispara cuando el texto de la SearchBar cambia.
        // Llama al comando de búsqueda del ViewModel con el nuevo texto.
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            await VistaModelo!.BuscarArticulosCommand.ExecuteAsync(e.NewTextValue);
        }

        // Evento que se dispara cuando cambia el estado del CheckBox (marcado/no marcado).
        // Obtiene el artículo al que pertenece ese checkbox y llama al comando
        // para alternar el estado de "Comprado" en el ViewModel.
        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is Articulo articulo)
            {
                await VistaModelo!.AlternarCompradoCommand.ExecuteAsync(articulo);
            }
        }
    }
}

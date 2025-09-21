using Listify.Servicios;
using Listify.ViewModels;

namespace Listify.Vistas
{
    // Esta es la parte de "code-behind" de la página EditarArticuloPagina.
    // Aquí se conecta la vista (XAML) con el ViewModel.
    // Básicamente, al cargar la página se establece el BindingContext.
    public partial class EditarArticuloPagina : ContentPage
    {
        public EditarArticuloPagina()
        {
            InitializeComponent();

            // El BindingContext se obtiene usando el ServicioLocalizador.
            // Esto nos da una instancia de EditarArticuloVistaModelo,
            // que es donde está la lógica para guardar/cancelar artículos.
            BindingContext = ServicioLocalizador.Obtener<EditarArticuloVistaModelo>();
        }
    }
}
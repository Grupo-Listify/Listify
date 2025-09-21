using Listify.Servicios;
using Listify.ViewModels;

namespace Listify.Vistas
{
    // Esta es la parte de "code-behind" de la p�gina EditarArticuloPagina.
    // Aqu� se conecta la vista (XAML) con el ViewModel.
    // B�sicamente, al cargar la p�gina se establece el BindingContext.
    public partial class EditarArticuloPagina : ContentPage
    {
        public EditarArticuloPagina()
        {
            InitializeComponent();

            // El BindingContext se obtiene usando el ServicioLocalizador.
            // Esto nos da una instancia de EditarArticuloVistaModelo,
            // que es donde est� la l�gica para guardar/cancelar art�culos.
            BindingContext = ServicioLocalizador.Obtener<EditarArticuloVistaModelo>();
        }
    }
}
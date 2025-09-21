using Listify.Vistas;

namespace Listify
{
    // Esta clase es el code-behind del AppShell.xaml.
    // AppShell se encarga de manejar la navegación principal de la aplicación.
    // Aquí podemos registrar rutas para que la app sepa cómo moverse entre páginas.
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registramos la ruta para la página de edición de artículos.
            // Esto permite navegar hacia EditarArticuloPagina con:
            // await Shell.Current.GoToAsync("EditarArticuloPagina");
            Routing.RegisterRoute("EditarArticuloPagina", typeof(EditarArticuloPagina));
        }
    }
}

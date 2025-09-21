namespace Listify
{
    /// <summary>
    /// Clase App: arranque de la aplicacion MAUI.
    /// Carga los recursos de App.xaml y define la pagina raiz.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Constructor por defecto.
        /// Inicializa los componentes y establece la pagina principal.
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Pagina raiz de la app.
            // AppShell maneja la navegacion y las rutas.
            MainPage = new AppShell();
        }
    }
}

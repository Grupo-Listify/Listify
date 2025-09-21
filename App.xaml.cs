namespace Listify
{
    // Esta clase es el punto de entrada principal de la app.
    // Aquí se inicializa todo y se define cuál es la primera página que se va a mostrar.
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establece la página inicial de la aplicación.
            // En este caso, usamos AppShell, que organiza la navegación
            // y las distintas páginas de la app.
            MainPage = new AppShell();
        }
    }
}
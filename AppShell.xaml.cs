using Microsoft.Maui.Controls;
using Listify.Vistas;

namespace Listify;

/// <summary>
/// AppShell: contenedor de navegacion de la aplicacion.
/// Define rutas y estructura base de la UI.
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Constructor por defecto.
    /// Inicializa los componentes y registra rutas de navegacion.
    /// </summary>
    public AppShell()
    {
        InitializeComponent();

        // Registra la ruta para navegar a la pagina de edicion de articulos
        Routing.RegisterRoute("EditarArticuloPagina", typeof(EditarArticuloPagina));
    }
}

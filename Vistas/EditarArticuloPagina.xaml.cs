using Microsoft.Maui.Controls;
using Listify.Servicios;
using Listify.ViewModels;

namespace Listify.Vistas;

/// <summary>
/// Pagina para crear o editar un articulo.
/// Define el BindingContext usando el servicio localizador (DI) para obtener el ViewModel.
/// </summary>
public partial class EditarArticuloPagina : ContentPage
{
    /// <summary>
    /// Constructor por defecto.
    /// Inicializa los componentes definidos en XAML y asigna el ViewModel de la pagina.
    /// </summary>
    public EditarArticuloPagina()
    {
        InitializeComponent();

        // Obtiene una instancia de EditarArticuloVistaModelo desde el contenedor de servicios
        // y la asigna como contexto de enlace (BindingContext) para los bindings de la UI.
        BindingContext = ServicioLocalizador.Obtener<EditarArticuloVistaModelo>();
    }
}

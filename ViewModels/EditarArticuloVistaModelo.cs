using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
namespace Listify.ViewModels
{
    // Este ViewModel se usa para la pantalla de editar o agregar un artículo.
    // Aquí tenemos toda la lógica para guardar cambios o cancelar la acción.
    [QueryProperty(nameof(Articulo), "Articulo")]
    public partial class EditarArticuloVistaModelo : ObservableObject
    {
        // Guardamos una referencia al servicio de base de datos.
        private readonly BaseDeDatosServicio _baseDeDatos;

        // Esta propiedad es el artículo que estamos editando o creando.
        // Con [ObservableProperty] se generan automáticamente
        // el getter, setter y la notificación a la UI cuando cambia.
        [ObservableProperty]
        private Articulo articulo = new();

        // Constructor principal: recibe la base de datos por inyección de dependencias.
        public EditarArticuloVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
        }

        // Constructor vacío, por si en algún momento se necesita.
        public EditarArticuloVistaModelo()
        {
            // no hace nada
        }

        // Comando para guardar los cambios en el artículo.
        // Si el nombre está vacío muestra un error.
        // Si el Id es 0 significa que es nuevo, entonces se agrega.
        // Si ya existe, se actualiza.
        // Al final vuelve a la pantalla anterior.
        [RelayCommand]
        private async Task GuardarAsync()
        {
            if (string.IsNullOrWhiteSpace(Articulo.Nombre))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            if (Articulo.Id == 0)
                await _baseDeDatos.AgregarArticuloAsync(Articulo);
            else
                await _baseDeDatos.ActualizarArticuloAsync(Articulo);

            await Shell.Current.GoToAsync(".."); // Volver atrás
        }

        // Comando para cancelar la edición.
        // Simplemente regresa a la pantalla anterior sin guardar nada.
        [RelayCommand]
        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}


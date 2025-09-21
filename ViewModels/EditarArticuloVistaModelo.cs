using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Listify.ViewModels
{
    /// <summary>
    /// ViewModel para crear o editar un articulo.
    /// Maneja estado de la pantalla, validaciones basicas y guardado en BD.
    /// Usa CommunityToolkit.Mvvm para propiedades observables y comandos.
    /// </summary>
    /// <remarks>
    /// - Navegacion: Shell.GoToAsync("..") para volver atras.
    /// - Paso de datos: QueryProperty permite recibir un Articulo por navegacion.
    /// - Comandos generados por atributos [RelayCommand]:
    ///     * GuardarAsyncCommand
    ///     * CancelarAsyncCommand
    /// </remarks>
    [QueryProperty(nameof(Articulo), "Articulo")]
    public partial class EditarArticuloVistaModelo : ObservableObject
    {
        // Servicio de acceso a la base de datos (inyeccion de dependencias)
        private readonly BaseDeDatosServicio _baseDeDatos;

        /// <summary>
        /// Articulo que se esta editando o creando.
        /// La anotacion [ObservableProperty] genera notificaciones de cambio.
        /// </summary>
        [ObservableProperty]
        private Articulo articulo = new();

        /// <summary>
        /// Constructor principal con el servicio de base de datos.
        /// Usado por el contenedor de DI.
        /// </summary>
        /// <param name="baseDeDatos">Servicio para operaciones CRUD de articulos.</param>
        public EditarArticuloVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
        }

        /// <summary>
        /// Constructor sin parametros requerido por algunos motores de navegacion o XAML.
        /// </summary>
        public EditarArticuloVistaModelo()
        {
            // sin inicializacion especial
        }

        /// <summary>
        /// Guarda el articulo actual.
        /// - Valida que el Nombre no este vacio.
        /// - Si Id es 0: inserta. Si no: actualiza.
        /// - Luego vuelve a la pantalla anterior.
        /// </summary>
        [RelayCommand]
        private async Task GuardarAsync()
        {
            // validacion simple: el nombre es obligatorio
            if (string.IsNullOrWhiteSpace(Articulo.Nombre))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            // decidir insertar o actualizar segun el Id
            if (Articulo.Id == 0)
                await _baseDeDatos.AgregarArticuloAsync(Articulo);
            else
                await _baseDeDatos.ActualizarArticuloAsync(Articulo);

            // volver atras en la pila de navegacion
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// Cancela la edicion y vuelve atras sin guardar cambios.
        /// </summary>
        [RelayCommand]
        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

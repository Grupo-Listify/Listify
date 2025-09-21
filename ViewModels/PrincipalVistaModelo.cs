using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
using Microsoft.Maui.Controls;

namespace Listify.ViewModels
{
    /// <summary>
    /// ViewModel principal de la app.
    /// Maneja la lista de articulos, la busqueda y las acciones de agregar, editar, eliminar y marcar como comprado.
    /// Usa CommunityToolkit.Mvvm para crear propiedades observables y comandos de forma sencilla.
    /// </summary>
    public partial class PrincipalVistaModelo : ObservableObject
    {
        // Servicio de base de datos inyectado por DI
        private readonly BaseDeDatosServicio _baseDeDatos;

        /// <summary>
        /// Texto que se usa para filtrar la lista por nombre.
        /// La propiedad es observable, asi la UI se entera de cambios.
        /// </summary>
        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        /// <summary>
        /// Coleccion que se muestra en la UI.
        /// Se actualiza al cargar, buscar, eliminar o cambiar estado.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Articulo> articulos = new();

        /// <summary>
        /// Constructor que recibe el servicio de base de datos.
        /// Dispara una carga inicial de articulos.
        /// </summary>
        /// <param name="baseDeDatos">Servicio para operaciones CRUD.</param>
        public PrincipalVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
            // Carga inicial (no bloquea la UI)
            _ = CargarArticulosAsync();
        }

        /// <summary>
        /// Lee todos los articulos de la base de datos y actualiza la coleccion visible.
        /// </summary>
        [RelayCommand]
        private async Task CargarArticulosAsync()
        {
            var lista = await _baseDeDatos.ObtenerArticulosAsync();
            Articulos = new ObservableCollection<Articulo>(lista);
        }

        /// <summary>
        /// Busca articulos por texto. Si el texto esta vacio, recarga todo.
        /// </summary>
        /// <param name="texto">Parte del nombre a buscar.</param>
        [RelayCommand]
        private async Task BuscarArticulosAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                await CargarArticulosAsync();
                return;
            }

            var resultados = await _baseDeDatos.BuscarArticulosAsync(texto);
            Articulos = new ObservableCollection<Articulo>(resultados);
        }

        /// <summary>
        /// Navega a la pagina de edicion para crear un nuevo articulo.
        /// </summary>
        [RelayCommand]
        private async Task AgregarArticuloAsync()
        {
            await Shell.Current.GoToAsync("EditarArticuloPagina");
        }

        /// <summary>
        /// Abre la pagina de edicion con el articulo seleccionado.
        /// </summary>
        /// <param name="articulo">Articulo a editar.</param>
        [RelayCommand]
        private async Task EditarArticuloAsync(Articulo articulo)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Articulo", articulo }
            };

            await Shell.Current.GoToAsync("EditarArticuloPagina", parametros);
        }

        /// <summary>
        /// Pide confirmacion, elimina el articulo de la base y lo quita de la lista si todo sale bien.
        /// </summary>
        /// <param name="articulo">Articulo a eliminar.</param>
        [RelayCommand]
        private async Task EliminarArticuloAsync(Articulo articulo)
        {
            if (articulo == null) return;

            bool confirmar = await App.Current.MainPage.DisplayAlert(
                "Eliminar",
                $"Eliminar '{articulo.Nombre}'?",
                "Si", "No");

            if (!confirmar) return;

            try
            {
                await _baseDeDatos.EliminarArticuloAsync(articulo);

                if (Articulos.Contains(articulo))
                {
                    Articulos.Remove(articulo);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error eliminando: {ex}");
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el articulo.", "OK");
            }
        }

        /// <summary>
        /// Cambia el estado Comprado del articulo y lo guarda.
        /// Si hay error, revierte el cambio local.
        /// </summary>
        /// <param name="articulo">Articulo a actualizar.</param>
        [RelayCommand]
        private async Task AlternarCompradoAsync(Articulo articulo)
        {
            if (articulo == null) return;

            try
            {
                articulo.Comprado = !articulo.Comprado;
                await _baseDeDatos.ActualizarArticuloAsync(articulo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error actualizando comprado: {ex}");
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el estado.", "OK");
                // revertir cambio local si falla
                articulo.Comprado = !articulo.Comprado;
            }
        }

        /// <summary>
        /// Establece el estado Comprado de forma explicita y guarda en base de datos.
        /// </summary>
        /// <param name="articulo">Articulo a actualizar.</param>
        /// <param name="comprado">true para comprado, false para pendiente.</param>
        public async Task EstablecerCompradoAsync(Articulo articulo, bool comprado)
        {
            if (articulo == null) return;

            articulo.Comprado = comprado;
            await _baseDeDatos.ActualizarArticuloAsync(articulo);
        }
    }
}

using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Maui.ApplicationModel; // si usas MainThread (opcional)
using System;

namespace Listify.ViewModels
{
    public partial class PrincipalVistaModelo : ObservableObject
    {
        private readonly BaseDeDatosServicio _baseDeDatos;

        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Articulo> articulos = new();

        public PrincipalVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
            _ = CargarArticulosAsync();
        }

        [RelayCommand]
        private async Task CargarArticulosAsync()
        {
            var lista = await _baseDeDatos.ObtenerArticulosAsync();
            Articulos = new ObservableCollection<Articulo>(lista);
        }

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

        [RelayCommand]
        private async Task AgregarArticuloAsync()
        {
            await Shell.Current.GoToAsync("EditarArticuloPagina");
        }

        [RelayCommand]
        private async Task EditarArticuloAsync(Articulo articulo)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Articulo", articulo }
            };

            await Shell.Current.GoToAsync("EditarArticuloPagina", parametros);
        }

        [RelayCommand]
        private async Task EliminarArticuloAsync(Articulo articulo)
        {
            if (articulo == null) return;

            bool confirmar = await App.Current.MainPage.DisplayAlert(
                "Eliminar",
                $"¿Eliminar '{articulo.Nombre}'?",
                "Sí", "No");

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
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el artículo.", "OK");
            }
        }

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
                articulo.Comprado = !articulo.Comprado;
            }
        }

        public async Task EstablecerCompradoAsync(Articulo articulo, bool comprado)
        {
            if (articulo == null) return;

            articulo.Comprado = comprado;
            await _baseDeDatos.ActualizarArticuloAsync(articulo);
        }
    }
}


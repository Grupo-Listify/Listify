using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
namespace Listify.ViewModels
{
    // Este ViewModel controla la pantalla principal de la app,
    // donde se muestran todos los artículos de la lista de compras.
    // Aquí está la lógica para buscar, agregar, editar, eliminar
    // y marcar artículos como comprados.
    public partial class PrincipalVistaModelo : ObservableObject
    {
        // Servicio de base de datos para guardar/leer artículos.
        private readonly BaseDeDatosServicio _baseDeDatos;

        // Texto que se escribe en la barra de búsqueda.
        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        // Lista de artículos que se muestra en la pantalla.
        // Es ObservableCollection para que la UI se actualice automáticamente
        // cuando cambien los datos.
        [ObservableProperty]
        private ObservableCollection<Articulo> articulos = new();

        // Constructor: recibe el servicio de base de datos e inmediatamente
        // carga los artículos guardados.
        public PrincipalVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
            _ = CargarArticulosAsync();
        }

        // Carga todos los artículos ordenados por nombre.
        [RelayCommand]
        private async Task CargarArticulosAsync()
        {
            var lista = await _baseDeDatos.ObtenerArticulosAsync();
            Articulos = new ObservableCollection<Articulo>(lista);
        }

        // Busca artículos por texto (ignora mayúsculas/minúsculas).
        // Si no hay texto, vuelve a cargar la lista completa.
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

        // Navega a la pantalla para agregar un nuevo artículo.
        [RelayCommand]
        private async Task AgregarArticuloAsync()
        {
            await Shell.Current.GoToAsync("EditarArticuloPagina");
        }

        // Navega a la pantalla de edición de un artículo existente,
        // pasándole el artículo como parámetro.
        [RelayCommand]
        private async Task EditarArticuloAsync(Articulo articulo)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Articulo", articulo }
            };

            await Shell.Current.GoToAsync("EditarArticuloPagina", parametros);
        }

        // Pregunta al usuario si quiere eliminar un artículo.
        // Si dice que sí, lo borra de la base de datos y recarga la lista.
        [RelayCommand]
        private async Task EliminarArticuloAsync(Articulo articulo)
        {
            bool confirmar = await App.Current.MainPage.DisplayAlert(
                "Eliminar",
                $"¿Eliminar '{articulo.Nombre}'?",
                "Sí", "No");

            if (confirmar)
            {
                await _baseDeDatos.EliminarArticuloAsync(articulo);
                await CargarArticulosAsync();
            }
        }

        // Cambia el estado de un artículo (comprado/no comprado),
        // guarda el cambio y actualiza la lista.
        [RelayCommand]
        private async Task AlternarCompradoAsync(Articulo articulo)
        {
            articulo.Comprado = !articulo.Comprado;
            await _baseDeDatos.ActualizarArticuloAsync(articulo);
            await CargarArticulosAsync();
        }
    }
}
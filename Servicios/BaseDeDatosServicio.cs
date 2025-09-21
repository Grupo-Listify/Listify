using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Listify.Modelos;

namespace Listify.Servicios
{
    // Esta clase maneja todo lo relacionado con la base de datos SQLite.
    // Aquí abrimos la conexión, creamos la tabla de artículos y hacemos
    // las operaciones básicas: agregar, actualizar, borrar y buscar.
    public class BaseDeDatosServicio
    {
        // Conexión a la base de datos, se guarda en esta variable privada.
        private SQLiteAsyncConnection _conexion;

        // Crea la conexión con el archivo compras.db3 si aún no existe.
        // También asegura que la tabla "Articulo" esté creada.
        private async Task CrearConexionAsync()
        {
            if (_conexion != null)
                return; // ya existe la conexión

            string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "compras.db3");
            _conexion = new SQLiteAsyncConnection(rutaBD);
            await _conexion.CreateTableAsync<Articulo>();
        }

        // Método público para inicializar la base de datos.
        public async Task InicializarAsync()
        {
            await CrearConexionAsync();
        }

        // Devuelve todos los artículos ordenados por nombre.
        public async Task<List<Articulo>> ObtenerArticulosAsync()
        {
            await CrearConexionAsync();
            return await _conexion.Table<Articulo>().OrderBy(a => a.Nombre).ToListAsync();
        }

        // Busca artículos cuyo nombre contenga el texto indicado (no importa mayúsculas/minúsculas).
        public async Task<List<Articulo>> BuscarArticulosAsync(string texto)
        {
            await CrearConexionAsync();
            return await _conexion.Table<Articulo>()
                .Where(a => a.Nombre.ToLower().Contains(texto.ToLower()))
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        // Inserta un nuevo artículo en la base de datos.
        public async Task AgregarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.InsertAsync(articulo);
        }

        // Actualiza un artículo que ya existe (por ejemplo, cambiar cantidad o estado de comprado).
        public async Task ActualizarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.UpdateAsync(articulo);
        }

        // Elimina un artículo de la lista de compras.
        public async Task EliminarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.DeleteAsync(articulo);
        }
    }
}


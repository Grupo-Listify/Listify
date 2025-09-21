using System.ComponentModel;
using SQLite;

namespace Listify.Modelos
{
    /// <summary>
    /// Modelo de datos para un articulo en una lista.
    /// Guarda la info en SQLite y notifica cambios a la UI (patron MVVM).
    /// </summary>
    /// <remarks>
    /// Tabla en SQLite: "articulos"
    /// Clave primaria: Id (autoincremental)
    /// Propiedades observables: Nombre, Cantidad, Comprado
    /// </remarks>
    [Table("articulos")]
    public class Articulo : INotifyPropertyChanged
    {
        /// <summary>
        /// Identificador unico del articulo en la base de datos.
        /// Se crea de forma automatica al guardar.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Campo privado para almacenar el nombre
        private string nombre = string.Empty;

        /// <summary>
        /// Nombre del articulo. No puede ser nulo.
        /// Ejemplos: Pan, Leche, Huevos.
        /// </summary>
        /// <remarks>
        /// Al cambiar el valor se avisa a la UI con OnPropertyChanged para
        /// que se refresque cualquier enlace de datos (Binding).
        /// </remarks>
        [NotNull]
        public string Nombre
        {
            get => nombre;
            set
            {
                // Evita notificar si el valor no cambia
                if (nombre == value) return;
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        // Campo privado para la cantidad
        private int cantidad = 1;

        /// <summary>
        /// Cantidad del articulo a comprar. Valor por defecto: 1.
        /// </summary>
        /// <remarks>
        /// Se recomienda usar valores mayores que cero.
        /// </remarks>
        public int Cantidad
        {
            get => cantidad;
            set
            {
                // Evita notificar si el valor no cambia
                if (cantidad == value) return;
                cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
            }
        }

        // Campo privado para el estado de compra
        private bool comprado = false;

        /// <summary>
        /// Indica si el articulo ya fue comprado.
        /// true: comprado, false: pendiente.
        /// </summary>
        public bool Comprado
        {
            get => comprado;
            set
            {
                // Evita notificar si el valor no cambia
                if (comprado == value) return;
                comprado = value;
                OnPropertyChanged(nameof(Comprado));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando una propiedad cambia de valor.
        /// La UI suscrita puede actualizar los elementos visuales automaticamente.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifica a la UI que una propiedad cambio de valor.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que cambio.</param>
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

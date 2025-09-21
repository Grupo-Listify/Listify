using System.ComponentModel;
using SQLite;

namespace Listify.Modelos
{
    [Table("articulos")]
    public class Articulo : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string nombre = string.Empty;
        [NotNull]
        public string Nombre
        {
            get => nombre;
            set
            {
                if (nombre == value) return;
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        private int cantidad = 1;
        public int Cantidad
        {
            get => cantidad;
            set
            {
                if (cantidad == value) return;
                cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
            }
        }

        private bool comprado = false;
        public bool Comprado
        {
            get => comprado;
            set
            {
                if (comprado == value) return;
                comprado = value;
                OnPropertyChanged(nameof(Comprado));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

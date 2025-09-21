using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Listify.Modelos
{
    // Esta clase representa un artículo de la lista de compras.
    // Básicamente es la tabla "articulos" en SQLite.
    // Aquí guardamos el nombre, cuántos queremos y si ya se compró o no.
    [Table("articulos")]
    public class Articulo
    {
        // Id único del artículo.
        // SQLite lo genera automáticamente al insertar.
        // Si vale 0 significa que todavía no se ha guardado en la base de datos.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Nombre del artículo.
        // Es obligatorio, no debería estar vacío.
        // Ejemplo: "Leche", "Pan", etc.
        [NotNull]
        public string Nombre { get; set; } = string.Empty;

        // Cantidad de unidades que quiero comprar.
        // Por defecto es 1, pero puedo poner más.
        public int Cantidad { get; set; } = 1;

        // Indica si ya lo compré o todavía está pendiente.
        // Por defecto siempre es "false" (pendiente).
        public bool Comprado { get; set; } = false;
    }
}

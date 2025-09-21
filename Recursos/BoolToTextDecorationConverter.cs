using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Listify.Recursos
{
    // Esta clase es un "converter".
    // Sirve para cambiar un valor booleano (true/false)
    // a un estilo de texto en la interfaz.
    //
    // En este caso:
    // - Si el artículo está marcado como comprado (true),
    //   se devuelve "Strikethrough" (texto tachado).
    // - Si no está comprado (false),
    //   se devuelve "None" (texto normal).
    //
    // Esto se usa en XAML cuando quiero mostrar
    // los artículos tachados si ya los compré.
    public class BoolToTextDecorationConverter : IValueConverter
    {
        // Convierte el valor bool que viene de la propiedad "Comprado"
        // a una decoración de texto que la UI puede entender.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool comprado && comprado)
                return TextDecorations.Strikethrough;

            return TextDecorations.None;
        }

        // Este método sería para convertir al reves
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

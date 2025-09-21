using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Listify.Recursos
{
    /// <summary>
    /// Convierte un valor bool a una decoracion de texto.
    /// Si el bool es true, devuelve Strikethrough (texto tachado).
    /// Si es false, devuelve None (sin decoracion).
    /// </summary>
    /// <remarks>
    /// Uso comun: marcar articulos comprados con texto tachado en una lista.
    /// Ejemplo XAML:
    /// <ContentPage.Resources>
    ///   <ResourceDictionary>
    ///     <recursos:BoolToTextDecorationConverter x:Key="BoolToTextDecorationConverter" />
    ///   </ResourceDictionary>
    /// </ContentPage.Resources>
    ///
    /// <Label Text="{Binding Nombre}"
    ///        TextDecorations="{Binding Comprado, Converter={StaticResource BoolToTextDecorationConverter}}" />
    /// </remarks>
    public class BoolToTextDecorationConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un bool a TextDecorations.
        /// </summary>
        /// <param name="value">Valor de entrada. Se espera un bool.</param>
        /// <param name="targetType">Tipo de destino. Se ignora.</param>
        /// <param name="parameter">Parametro extra. No se usa.</param>
        /// <param name="culture">Cultura actual. No se usa.</param>
        /// <returns>
        /// TextDecorations.Strikethrough si value es true.
        /// TextDecorations.None si value es false o no es bool.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // si el articulo esta comprado, se tacha el texto
            if (value is bool comprado && comprado)
                return TextDecorations.Strikethrough;

            // si no esta comprado, sin decoracion
            return TextDecorations.None;
        }

        /// <summary>
        /// No se implementa porque no se necesita conversion inversa.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

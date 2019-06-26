using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static System.Windows.Application;

namespace MaterialDesign_WPF_Expander.Converters
{
    /// <summary>
    /// Convert between boolean and expander icon
    /// </summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    internal sealed class BooleanToExpanderIconConverter : IValueConverter
    {
        /// <summary>
        /// Convert bool to expander icon
        /// </summary>
        /// <param name="value">boolean value</param>
        /// <param name="targetType">Visibility</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns>Minus icon or Plus icon</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return value is bool boolean && boolean
                ? (ImageSource)Current.Resources["MinusIcon"]
                : (ImageSource)Current.Resources["PlusIcon"];
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}

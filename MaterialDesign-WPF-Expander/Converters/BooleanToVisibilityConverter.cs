using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesign_WPF_Expander.Converters
{
    /// <summary>
    /// Convert between boolean and visibility
    /// </summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    internal sealed class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert bool to Visibility
        /// </summary>
        /// <param name="value">boolean value</param>
        /// <param name="targetType">Visibility</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns>Visible or Hidden</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return value is bool boolean && boolean
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        /// <summary>
        /// Convert Visibility to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return value is Visibility visibility &&
                   visibility == Visibility.Visible;
        }
    }
}

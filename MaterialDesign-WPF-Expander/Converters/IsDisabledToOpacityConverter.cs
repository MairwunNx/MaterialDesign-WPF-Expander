using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesign_WPF_Expander.Converters
{
    [Localizability(LocalizationCategory.NeverLocalize)]
    internal sealed class IsDisabledToOpacityConverter : IValueConverter
    {
        /// <summary>
        /// Convert is disabled to disabled opacity
        /// </summary>
        /// <param name="value">boolean value</param>
        /// <param name="targetType">Double</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns>disabled opacity or normal opacity</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return value is bool boolean && boolean
                ? 1.0
                : 0.6;
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

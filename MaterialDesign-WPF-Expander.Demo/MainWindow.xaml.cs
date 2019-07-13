using System.Windows;
using System;
using MaterialDesign_WPF_Expander.EventArguments;

namespace MaterialDesign_WPF_Expander.Demo
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private bool _isDarkTheme = true;
        private readonly ResourceDictionary _dictLight = new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesign-WPF-Expander;component/Themes/LightDictionary.xaml", UriKind.Absolute)
        };
        private readonly ResourceDictionary _dictDark = new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesign-WPF-Expander;component/Themes/DarkDictionary.xaml", UriKind.Absolute)
        };
        private readonly ResourceDictionary _dictIcon = new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesign-WPF-Expander;component/Resources/Icons/Icons.xaml", UriKind.Absolute)
        };

        private void EasyIntegrationExpander_IsOpenedChangedEvent(object sender, IsOpenedChangedEventArgs e)
        {
            MessageBox.Show($"Opened state changed to: {e.IsOpened}");
        }

        private void EasyIntegrationExpander_ClosedEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Closed!");
        }

        private void EasyIntegrationExpander_CloseEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Closing!");
        }

        private void EasyIntegrationExpander_OpenedEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Opened!");
        }

        private void EasyIntegrationExpander_OpenEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Opening!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isDarkTheme)
            {
                _isDarkTheme = false;
                Application.Current.Resources.MergedDictionaries.Remove(_dictDark);
                Application.Current.Resources.MergedDictionaries.Add(_dictLight);

                //Application.Current.Resources.MergedDictionaries.Remove(_dictIcon);
                //Application.Current.Resources.MergedDictionaries.Add(_dictIcon);
            }
            else
            {
                _isDarkTheme = true;
                Application.Current.Resources.MergedDictionaries.Remove(_dictLight);
                Application.Current.Resources.MergedDictionaries.Add(_dictDark);
                
                //Application.Current.Resources.MergedDictionaries.Remove(_dictIcon);
                //Application.Current.Resources.MergedDictionaries.Add(_dictIcon);
            }
        }
    }
}

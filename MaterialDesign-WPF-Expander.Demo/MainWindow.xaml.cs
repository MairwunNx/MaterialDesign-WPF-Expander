using System.Windows;

namespace MaterialDesign_WPF_Expander.Demo
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void SwitchIsOpenToggleButton_Click(object sender, RoutedEventArgs e)
        {
            PrivacyExpander.ExpanderIsOpened =
                SwitchIsOpenToggleButton.IsChecked == true;
        }
    }
}

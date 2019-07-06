using System.Windows;

namespace MaterialDesign_WPF_Expander.Demo
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void EasyIntegrationExpander_IsOpenedChangedEvent(object sender, EventArgs.IsOpenedChangedEventArgs e)
        {
            MessageBox.Show($"Opened state changed to: {e.IsOpened}");
        }

        private void EasyIntegrationExpander_ClosedEvent(object sender, System.EventArgs e)
        {
            MessageBox.Show("Closed!");
        }

        private void EasyIntegrationExpander_CloseEvent(object sender, System.EventArgs e)
        {
            MessageBox.Show("Closing!");
        }

        private void EasyIntegrationExpander_OpenedEvent(object sender, System.EventArgs e)
        {
            MessageBox.Show("Opened!");
        }

        private void EasyIntegrationExpander_OpenEvent(object sender, System.EventArgs e)
        {
            MessageBox.Show("Opening!");
        }
    }
}

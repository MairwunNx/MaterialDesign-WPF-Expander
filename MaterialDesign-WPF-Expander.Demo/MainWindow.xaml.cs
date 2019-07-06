using System.Windows;
using System;
using MaterialDesign_WPF_Expander.EventArguments;

namespace MaterialDesign_WPF_Expander.Demo
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

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
    }
}

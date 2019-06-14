using System.Windows;
using System.Windows.Input;

namespace MaterialDesign_WPF_Expander.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("clicked");
        }
    }
}

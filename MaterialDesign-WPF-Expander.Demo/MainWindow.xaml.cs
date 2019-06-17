﻿using System.Windows;

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

        private void DisableBorderToggleButton_Click(object sender, RoutedEventArgs e)
        {
            PrivacyExpander.ExpanderBottomBorderIsVisible = DisableBorderToggleButton.IsChecked == true;
        }

        private void SwitchIsOpenToggleButton_Click(object sender, RoutedEventArgs e)
        {
            PrivacyExpander.ExpanderIsOpened = SwitchIsOpenToggleButton.IsChecked == true;
        }

        private void SwitchBorderThickness_Click(object sender, RoutedEventArgs e)
        {
            PrivacyExpander.ExpanderBottomBorderThickness = SwitchBorderThickness.IsChecked == true ? 3.0 : 1.0;
        }

        private bool _opened = false;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_opened)
            {
                BaseStack.Height = BaseButton.ActualHeight;
                _opened = false;
            }
            else
            {
                BaseStack.Height = BaseButton.ActualHeight + BaseContent.ActualHeight;
                _opened = true;
            }
        }
    }
}
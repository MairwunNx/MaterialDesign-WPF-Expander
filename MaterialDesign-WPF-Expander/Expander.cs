using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Windows.Application;
#pragma warning disable 1591

namespace MaterialDesign_WPF_Expander
{
    public class Expander : Control
    {
        public string ExpanderGroup
        {
            get => GetValue(ExpanderGroupProperty) as string;
            set => SetValue(ExpanderGroupProperty, value);
        }

        public static readonly DependencyProperty ExpanderGroupProperty =
            DependencyProperty.Register(
                "ExpanderGroup",
                typeof(string),
                typeof(Expander),
                new PropertyMetadata("default")
            );

        public bool ExpanderOnlyOneOpenedObject
        {
            get => (bool) GetValue(ExpanderOnlyOneOpenedObjectProperty);
            set => SetValue(ExpanderOnlyOneOpenedObjectProperty, value);
        }

        public static readonly DependencyProperty ExpanderOnlyOneOpenedObjectProperty =
            DependencyProperty.Register(
                "ExpanderOnlyOneOpenedObject",
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(true)
            );


        public bool ExpanderIconIsVisible
        {
            get => (bool) GetValue(ExpanderIconIsVisibleProperty);
            set => SetValue(ExpanderIconIsVisibleProperty, value);
        }

        public static readonly DependencyProperty ExpanderIconIsVisibleProperty =
            DependencyProperty.Register(
                "ExpanderIconIsVisible",
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(true)
            );


        /*public DrawingImage ExpanderExpandIcon
        {
            get => (DrawingImage) GetValue(ExpanderExpandIconProperty);
            set => SetValue(ExpanderExpandIconProperty, value);
        }*/

        /*public static readonly DependencyProperty ExpanderExpandIconProperty =
            DependencyProperty.Register(
                "ExpanderExpandIcon",
                typeof(DrawingImage),
                typeof(Expander),
                new PropertyMetadata(Current.Resources["PlusIcon"] as DrawingImage)
            );*/


        public bool ExpanderIsOpened
        {
            get => (bool) GetValue(ExpanderIsOpenedProperty);
            set => SetValue(ExpanderIsOpenedProperty, value);
        }

        public static readonly DependencyProperty ExpanderIsOpenedProperty =
            DependencyProperty.Register(
                "ExpanderIsOpen",
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(false)
            );


        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Expander),
                new FrameworkPropertyMetadata(typeof(Expander))
            );
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#pragma warning disable 1591

namespace MaterialDesign_WPF_Expander
{
    [ContentProperty("Content")]
    public class Expander : Control
    {
        public string ExpanderGroup
        {
            get => GetValue(ExpanderGroupProperty) as string;
            set => SetValue(ExpanderGroupProperty, value);
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Expander), null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Expander), new PropertyMetadata("Expander"));


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

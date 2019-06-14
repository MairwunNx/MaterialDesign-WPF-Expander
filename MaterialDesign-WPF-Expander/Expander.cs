using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesign_WPF_Expander
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     <code>xmlns:materialDesignExpander="clr-namespace:MaterialDesign_WPF_Expander"</code>
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     <code>xmlns:materialDesignExpander="clr-namespace:MaterialDesign_WPF_Expander;assembly=MaterialDesign_WPF_Expander"</code>
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <code>&lt;materialDesignExpander:Expander/&gt;</code>
    ///
    /// </summary>
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
            get { return (bool)GetValue(ExpanderOnlyOneOpenedObjectProperty); }
            set { SetValue(ExpanderOnlyOneOpenedObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpanderOnlyOneOpenedObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderOnlyOneOpenedObjectProperty =
            DependencyProperty.Register("ExpanderOnlyOneOpenedObject", typeof(bool), typeof(Expander), new PropertyMetadata(true));



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


        public DrawingImage ExpanderExpandIcon
        {
            get => (DrawingImage) GetValue(ExpanderExpandIconProperty);
            set => SetValue(ExpanderExpandIconProperty, value);
        }

        // Using a DependencyProperty as the backing store for ExpanderExpandIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderExpandIconProperty =
            DependencyProperty.Register("ExpanderExpandIcon", typeof(DrawingImage), typeof(Expander), new PropertyMetadata(Application.Current.Resources["PlusIcon"]));



        public bool ExpanderIsOpened
        {
            get { return (bool)GetValue(ExpanderIsOpenedProperty); }
            set { SetValue(ExpanderIsOpenedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpanderIsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderIsOpenedProperty =
            DependencyProperty.Register("ExpanderIsOpen", typeof(bool), typeof(Expander), new PropertyMetadata(false));



        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Expander),
                new FrameworkPropertyMetadata(typeof(Expander))
            );
        }
    }
}

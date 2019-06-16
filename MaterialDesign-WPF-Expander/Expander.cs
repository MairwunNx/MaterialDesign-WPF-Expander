using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(Expander), null);

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(Expander),
                new PropertyMetadata("Expander"));


        public static readonly DependencyProperty ExpanderGroupProperty =
            DependencyProperty.Register(
                nameof(ExpanderGroup),
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
                nameof(ExpanderOnlyOneOpenedObject),
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
                nameof(ExpanderIconIsVisible),
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
                nameof(ExpanderIsOpened),
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(false)
            );

        public int OpenAnimationDuration
        {
            get { return (int) GetValue(OpenAnimationDurationProperty); }
            set { SetValue(OpenAnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenAnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenAnimationDurationProperty =
            DependencyProperty.Register(nameof(OpenAnimationDuration), typeof(int),
                typeof(Expander), new PropertyMetadata(0));


        public override void OnApplyTemplate()
        {
            TextBlock expanderHeader = GetTemplateChild("ExpanderHeader") as TextBlock;
            Grid expanderGrid = GetTemplateChild("ExpanderGrid") as Grid;
            Grid contentGrid = GetTemplateChild("ContentGrid") as Grid;
            Image expanderIcon = GetTemplateChild("ExpanderIcon") as Image;
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetName(expanderGrid, expanderGrid.Name);

            expanderHeader.MouseLeftButtonUp += (s, e) =>
            {
                if (ExpanderIsOpened)
                {
                    expanderIcon.Source = (ImageSource) Application.Current.Resources["PlusIcon"];
                    /*
                DoubleAnimation doubleAnimation = new DoubleAnimation
                {
                    To = expanderHeader.ActualHeight,
                    From = expanderGrid.ActualHeight,
                    Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration)),
                };

                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(HeightProperty));
                storyboard.Children.Add(doubleAnimation);*/
                    ExpanderIsOpened = false;
//                    storyboard.Completed +=(sender, args) => ExpanderIsOpened = false;
//                    storyboard.Begin(expanderGrid);
                }
                else
                {
                    expanderIcon.Source = (ImageSource) Application.Current.Resources["MinusIcon"];
//                    DoubleAnimation doubleAnimation = new DoubleAnimation
//                    {
//                        To = contentGrid.ActualHeight + expanderHeader.ActualHeight,
//                        From = expanderHeader.ActualHeight,
//                        Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration)),
//                    };
//
//                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(HeightProperty));
//                    storyboard.Children.Add(doubleAnimation);

                    ExpanderIsOpened = true;
//                    storyboard.Begin(expanderGrid);
                }
            };
        }

        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Expander),
                new FrameworkPropertyMetadata(typeof(Expander))
            );
        }
    }
}

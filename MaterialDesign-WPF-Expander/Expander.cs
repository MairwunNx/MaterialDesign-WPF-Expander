using System;
using System.Diagnostics;
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
        private Image _expanderIcon;
        private Border _expanderBorder;
        private TextBlock _expanderHeader;
        private Grid _expanderGrid;
        private Grid _contentGrid;
        private ContentPresenter _contentPresenter;

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
            get { return GetValue(TitleProperty) as string; }
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
            set
            {
                Storyboard.SetTargetName(_expanderGrid, _expanderGrid.Name);

                if (value)
                {
                    SetValue(ExpanderIsOpenedProperty, true);

                    DoubleAnimation doubleAnimation = new DoubleAnimation
                    {
                        To = _expanderGrid.ActualHeight,
                        From = _expanderHeader.ActualHeight,
                        Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration)),
                    };

                    Storyboard.SetTargetProperty(
                        doubleAnimation,
                        new PropertyPath(HeightProperty)
                    );
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(doubleAnimation);
                    storyboard.Completed += (sender, args) =>
                    {
                        _expanderIcon.Source =
                            Application.Current.Resources["MinusIcon"] as ImageSource;
                    };
                    storyboard.Begin(_expanderGrid);
                }
                else
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation
                    {
                        To = _expanderHeader.ActualHeight,
                        From = _expanderGrid.ActualHeight,
                        Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration))
                    };

                    Storyboard.SetTargetProperty(
                        doubleAnimation,
                        new PropertyPath(HeightProperty)
                    );
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(doubleAnimation);
                    storyboard.Completed += (sender, args) =>
                    {
                        _expanderIcon.Source =
                            Application.Current.Resources["PlusIcon"] as ImageSource;
                        SetValue(ExpanderIsOpenedProperty, false);
                    };
                    storyboard.Begin(_expanderGrid);
                }
            }
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
                typeof(Expander), new PropertyMetadata(200));


        public double ExpanderIconZoom
        {
            get => (double) GetValue(ExpanderIconZoomProperty);
            set => SetValue(ExpanderIconZoomProperty, value);
        }

        public static readonly DependencyProperty ExpanderIconZoomProperty =
            DependencyProperty.Register(nameof(ExpanderIconZoom), typeof(double), typeof(Expander),
                new PropertyMetadata(0.7));

        private void InitExpanderIsOpenedIcon()
        {
            _expanderIcon.Source = ExpanderIsOpened
                ? (ImageSource) Application.Current.Resources["MinusIcon"]
                : (ImageSource) Application.Current.Resources["PlusIcon"];
        }

        private void InitExpanderHeightByIsOpened()
        {
            if (!ExpanderIsOpened)
            {
                _expanderGrid.Height = _expanderHeader.ActualHeight;
            }
        }

        private void AssignElements()
        {
            _expanderBorder = GetTemplateChild("ExpanderBorder") as Border;
            _expanderGrid = GetTemplateChild("ExpanderGrid") as Grid;
            _expanderIcon = GetTemplateChild("ExpanderIcon") as Image;
            _expanderHeader = GetTemplateChild("ExpanderHeader") as TextBlock;
            _contentGrid = GetTemplateChild("ContentGrid") as Grid;
            _contentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;
        }

        private void InitExpanderOnClickHandler()
        {
            _expanderHeader.MouseLeftButtonUp += (s, e) =>
            {
                ExpanderIsOpened = !ExpanderIsOpened;
            };
            _expanderIcon.MouseLeftButtonUp += (s, e) =>
            {
                ExpanderIsOpened = !ExpanderIsOpened;
            };
        }

        private void ProcessElementLoaded()
        {
            Loaded += (s, e) =>
            {
                InitExpanderIsOpenedIcon();
                InitExpanderHeightByIsOpened();
                InitExpanderOnClickHandler();
            };
        }

        public override void OnApplyTemplate()
        {
            AssignElements();
            ProcessElementLoaded();
        }

        public double ExpanderBottomBorderThickness
        {
            get { return (double) GetValue(ExpanderBottomBorderThicknessProperty); }
            set
            {
                SetValue(ExpanderBottomBorderThicknessProperty, value);

                if (ExpanderBottomBorderIsVisible)
                {
                    _expanderBorder.BorderThickness = new Thickness(0, 0, 0, value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for ExpanderBottomBorderThinkness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderBottomBorderThicknessProperty =
            DependencyProperty.Register("ExpanderBottomBorderThinkness", typeof(double),
                typeof(Expander), new PropertyMetadata(1.0));


        public bool ExpanderBottomBorderIsVisible
        {
            get { return (bool) GetValue(ExpanderBottomBorderIsVisibleProperty); }
            set
            {
                SetValue(ExpanderBottomBorderIsVisibleProperty, value);

                if (value)
                {
                    _expanderBorder.BorderThickness =
                        new Thickness(0, 0, 0, ExpanderBottomBorderThickness);
                }
                else
                {
                    ExpanderBottomBorderThickness = _expanderBorder.BorderThickness.Bottom;
                    _expanderBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                }
            }
        }

        // Using a DependencyProperty as the backing store for ExpanderBottomBorderIsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderBottomBorderIsVisibleProperty =
            DependencyProperty.Register(nameof(ExpanderBottomBorderIsVisible), typeof(bool),
                typeof(Expander), new PropertyMetadata(true));

        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Expander),
                new FrameworkPropertyMetadata(typeof(Expander))
            );
        }
    }
}

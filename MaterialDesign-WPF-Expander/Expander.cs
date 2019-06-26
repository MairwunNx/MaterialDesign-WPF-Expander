using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
            get => (bool)GetValue(ExpanderOnlyOneOpenedObjectProperty);
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
            get => (bool)GetValue(ExpanderIconIsVisibleProperty);
            set => SetValue(ExpanderIconIsVisibleProperty, value);
        }

        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register("HeaderHorizontalAlignment", typeof(HorizontalAlignment), typeof(Expander), new PropertyMetadata(HorizontalAlignment.Left));

        public static readonly DependencyProperty ExpanderIconIsVisibleProperty =
            DependencyProperty.Register(
                nameof(ExpanderIconIsVisible),
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(true)
            );

        public bool ExpanderIsOpened
        {
            get => (bool)GetValue(ExpanderIsOpenedProperty);
            set
            {
                Storyboard.SetTargetName(_expanderBorder, _expanderBorder.Name);

                if (value)
                {
                    SetValue(ExpanderIsOpenedProperty, true);

                    DoubleAnimation doubleAnimation = new DoubleAnimation
                    {
                        To = _expanderBorder.BorderThickness.Bottom +
                             _expanderBorder.Padding.Bottom +
                             _expanderHeader.ActualHeight +
                             _contentPresenter.ActualHeight,
                        From = _expanderBorder.BorderThickness.Bottom +
                               _expanderBorder.Padding.Bottom +
                               _expanderHeader.ActualHeight,
                        Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration)),
                    };

                    Storyboard.SetTargetProperty(
                        doubleAnimation,
                        new PropertyPath(HeightProperty)
                    );
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(doubleAnimation);
                    storyboard.FillBehavior = FillBehavior.Stop;
                    storyboard.Begin(_expanderBorder);
                }
                else
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation
                    {
                        To = _expanderBorder.BorderThickness.Bottom +
                             _expanderBorder.Padding.Bottom +
                             _expanderHeader.ActualHeight,
                        From = _expanderBorder.ActualHeight,
                        Duration = new Duration(TimeSpan.FromMilliseconds(OpenAnimationDuration))
                    };

                    Storyboard.SetTargetProperty(
                        doubleAnimation,
                        new PropertyPath(HeightProperty)
                    );
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(doubleAnimation);
                    storyboard.FillBehavior = FillBehavior.Stop;
                    storyboard.Completed += (sender, args) =>
                    {
                        SetValue(ExpanderIsOpenedProperty, false);
                    };
                    storyboard.Begin(_expanderBorder);
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
            get { return (int)GetValue(OpenAnimationDurationProperty); }
            set { SetValue(OpenAnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenAnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenAnimationDurationProperty =
            DependencyProperty.Register(nameof(OpenAnimationDuration), typeof(int),
                typeof(Expander), new PropertyMetadata(200));


        public double ExpanderIconZoom
        {
            get => (double)GetValue(ExpanderIconZoomProperty);
            set => SetValue(ExpanderIconZoomProperty, value);
        }

        public static readonly DependencyProperty ExpanderIconZoomProperty =
            DependencyProperty.Register(nameof(ExpanderIconZoom), typeof(double), typeof(Expander),
                new PropertyMetadata(0.7));

        private void InitExpanderHeightByIsOpened()
        {
            if (!ExpanderIsOpened)
            {
                _expanderBorder.Height =
                    _expanderHeader.ActualHeight +
                    _expanderBorder.Padding.Bottom +
                    _expanderBorder.BorderThickness.Bottom;
            }
        }

        private void AssignElements()
        {
            _expanderBorder = GetTemplateChild("ExpanderBorder") as Border;
            _expanderIcon = GetTemplateChild("ExpanderIcon") as Image;
            _expanderHeader = GetTemplateChild("ExpanderHeader") as TextBlock;
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

        private void ProcessExpanderResize()
        {
            _expanderBorder.SizeChanged += (o, eventArgs) =>
            {
                if (ExpanderIsOpened)
                {
                    _expanderBorder.Height =
                        _expanderBorder.BorderThickness.Bottom +
                        _expanderBorder.Padding.Bottom +
                        _expanderHeader.ActualHeight +
                        _contentPresenter.ActualHeight;
                }
                else
                {
                    _expanderBorder.Height =
                        _expanderHeader.ActualHeight +
                        _expanderBorder.Padding.Bottom +
                        _expanderBorder.BorderThickness.Bottom;
                }
            };
        }

        private void ProcessElementLoaded()
        {
            Loaded += (s, e) =>
            {
                InitExpanderHeightByIsOpened();
                InitExpanderOnClickHandler();
                ProcessExpanderResize();
            };
        }

        public override void OnApplyTemplate()
        {
            AssignElements();
            ProcessElementLoaded();
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

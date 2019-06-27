using System;
using System.ComponentModel;
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

        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Expander),
                new FrameworkPropertyMetadata(typeof(Expander))
            );
        }

        public string ExpanderGroup
        {
            get => GetValue(ExpanderGroupProperty) as string;
            set => SetValue(ExpanderGroupProperty, value);
        }

        [Category("Common")]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(object),
                typeof(Expander),
                null
            );

        [Category("Common")]
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(Expander),
                new PropertyMetadata("Expander")
            );

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

        [Category("Appearance")]
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

        [Category("Layout")]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get => (HorizontalAlignment) GetValue(HeaderHorizontalAlignmentProperty);
            set => SetValue(HeaderHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(HeaderHorizontalAlignment),
                typeof(HorizontalAlignment),
                typeof(Expander),
                new PropertyMetadata(HorizontalAlignment.Left)
            );

        [Category("Common")]
        public bool ExpanderIsOpened
        {
            get => (bool) GetValue(ExpanderIsOpenedProperty);
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

        [Category("Appearance")]
        public CornerRadius ExpanderCornerRadius
        {
            get => (CornerRadius) GetValue(ExpanderCornerRadiusProperty);
            set => SetValue(ExpanderCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ExpanderCornerRadiusProperty =
            DependencyProperty.Register(
                nameof(ExpanderCornerRadius),
                typeof(CornerRadius),
                typeof(Expander),
                new PropertyMetadata(new CornerRadius(0))
            );

        [Category("Behavior")]
        public int OpenAnimationDuration
        {
            get => (int) GetValue(OpenAnimationDurationProperty);
            set => SetValue(OpenAnimationDurationProperty, value);
        }

        public static readonly DependencyProperty OpenAnimationDurationProperty =
            DependencyProperty.Register(
                nameof(OpenAnimationDuration),
                typeof(int),
                typeof(Expander),
                new PropertyMetadata(200)
            );

        [Category("Appearance")]
        public double ExpanderIconZoom
        {
            get => (double) GetValue(ExpanderIconZoomProperty);
            set => SetValue(ExpanderIconZoomProperty, value);
        }

        public static readonly DependencyProperty ExpanderIconZoomProperty =
            DependencyProperty.Register(
                nameof(ExpanderIconZoom),
                typeof(double),
                typeof(Expander),
                new PropertyMetadata(0.7)
            );
        
        public override void OnApplyTemplate()
        {
            AssignElements();
            ProcessElementLoaded();
        }

        private void AssignElements()
        {
            _expanderBorder = GetTemplateChild("ExpanderBorder") as Border;
            _expanderIcon = GetTemplateChild("ExpanderIcon") as Image;
            _expanderHeader = GetTemplateChild("ExpanderHeader") as TextBlock;
            _contentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;
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

        private void InitExpanderOnClickHandler()
        {
            _expanderHeader.MouseLeftButtonUp += (s, e) => ExpanderIsOpened = !ExpanderIsOpened;
            _expanderIcon.MouseLeftButtonUp += (s, e) => ExpanderIsOpened = !ExpanderIsOpened;
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
    }
}

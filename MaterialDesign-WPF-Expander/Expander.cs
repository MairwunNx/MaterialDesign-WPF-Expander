using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;

#pragma warning disable 1591

namespace MaterialDesign_WPF_Expander
{
    /// <summary>
    /// 
    /// </summary>
    [ContentProperty("Content")]
    public class Expander : Control
    {
        private Image _expanderIcon;
        private Border _expanderBorder;
        private TextBlock _expanderHeader;
        private ContentPresenter _contentPresenter;
        private static readonly Type TypeofThis = typeof(Expander);

        static Expander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                TypeofThis,
                new FrameworkPropertyMetadata(TypeofThis)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpanderGroup
        {
            get => GetValue(ExpanderGroupProperty) as string;
            set => SetValue(ExpanderGroupProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ExpanderGroupProperty =
            DependencyProperty.Register(
                nameof(ExpanderGroup),
                typeof(string),
                TypeofThis,
                new PropertyMetadata("default")
            );

        /// <summary>
        ///
        /// </summary>
        [Category("Common")]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(object),
                TypeofThis,
                null
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Common")]
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                TypeofThis,
                new PropertyMetadata("Expander")
            );

        /// <summary>
        /// 
        /// </summary>
        public bool ExpanderOnlyOneOpenedObject
        {
            get => (bool) GetValue(ExpanderOnlyOneOpenedObjectProperty);
            set => SetValue(ExpanderOnlyOneOpenedObjectProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ExpanderOnlyOneOpenedObjectProperty =
            DependencyProperty.Register(
                nameof(ExpanderOnlyOneOpenedObject),
                typeof(bool),
                TypeofThis,
                new PropertyMetadata(true)
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public bool IconIsVisible
        {
            get => (bool) GetValue(IconIsVisibleProperty);
            set => SetValue(IconIsVisibleProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconIsVisibleProperty =
            DependencyProperty.Register(
                nameof(IconIsVisible),
                typeof(bool),
                TypeofThis,
                new PropertyMetadata(true)
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout")]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get => (HorizontalAlignment) GetValue(HeaderHorizontalAlignmentProperty);
            set => SetValue(HeaderHorizontalAlignmentProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(HeaderHorizontalAlignment),
                typeof(HorizontalAlignment),
                TypeofThis,
                new PropertyMetadata(HorizontalAlignment.Left)
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Common")]
        public bool IsOpened
        {
            get => (bool) GetValue(IsOpenedProperty);
            set
            {
                Storyboard.SetTargetName(_expanderBorder, _expanderBorder.Name);

                if (value)
                {
                    SetValue(IsOpenedProperty, true);

                    DoubleAnimation doubleAnimation = new DoubleAnimation
                    {
                        To = _expanderBorder.BorderThickness.Bottom +
                             _expanderBorder.Padding.Bottom +
                             _expanderHeader.ActualHeight +
                             _contentPresenter.ActualHeight,
                        From = _expanderBorder.BorderThickness.Bottom +
                               _expanderBorder.Padding.Bottom +
                               _expanderHeader.ActualHeight,
                        Duration = new Duration(TimeSpan.FromMilliseconds(AnimationDuration)),
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
                        Duration = new Duration(TimeSpan.FromMilliseconds(AnimationDuration))
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
                        SetValue(IsOpenedProperty, false);
                    };
                    storyboard.Begin(_expanderBorder);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register(
                nameof(IsOpened),
                typeof(bool),
                TypeofThis,
                new PropertyMetadata(false)
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                TypeofThis,
                new PropertyMetadata(new CornerRadius(0))
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public int AnimationDuration
        {
            get => (int) GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                typeof(int),
                TypeofThis,
                new PropertyMetadata(200)
            );

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public double IconZoom
        {
            get => (double) GetValue(IconZoomProperty);
            set => SetValue(IconZoomProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconZoomProperty =
            DependencyProperty.Register(
                nameof(IconZoom),
                typeof(double),
                TypeofThis,
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
            if (!IsOpened)
            {
                _expanderBorder.Height =
                    _expanderHeader.ActualHeight +
                    _expanderBorder.Padding.Bottom +
                    _expanderBorder.BorderThickness.Bottom;
            }
        }

        private void InitExpanderOnClickHandler()
        {
            _expanderHeader.MouseLeftButtonUp += (s, e) => IsOpened = !IsOpened;
            _expanderIcon.MouseLeftButtonUp += (s, e) => IsOpened = !IsOpened;
        }

        private void ProcessExpanderResize()
        {
            _expanderBorder.SizeChanged += (o, eventArgs) =>
            {
                if (IsOpened)
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

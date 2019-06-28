using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesign_WPF_Expander
{
    /// <summary>
    /// Base class for Material Design Expander control.
    /// </summary>
    [ContentProperty("Content")]
    public class Expander : Control
    {
        private const string DefaultHeaderFontName = "Segoe Ui";
        private const double DefaultHeaderFontSize = 24.0D;
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
        /// Represents a property to display any single element within the Expander.
        /// </summary>
        [Category("Common")]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Content"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(object),
                TypeofThis,
                null
            );

        /// <summary>
        /// Represents the property that controls the Expander header.
        /// </summary>
        [Category("Common")]
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                TypeofThis,
                new PropertyMetadata("Lorem ipsum dolor sit amet")
            );

        /// <summary>
        /// Controls the visibility of the Expander icon.
        /// </summary>
        [Category("Appearance")]
        public bool IconIsVisible
        {
            get => (bool)GetValue(IconIsVisibleProperty);
            set => SetValue(IconIsVisibleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IconIsVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconIsVisibleProperty =
            DependencyProperty.Register(
                nameof(IconIsVisible),
                typeof(bool),
                TypeofThis,
                new PropertyMetadata(true)
            );

        /// <summary>
        /// Controls the horizontal position of the header in Expander.
        /// </summary>
        [Category("Layout")]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty);
            set => SetValue(HeaderHorizontalAlignmentProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderHorizontalAlignment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(HeaderHorizontalAlignment),
                typeof(HorizontalAlignment),
                TypeofThis,
                new PropertyMetadata(HorizontalAlignment.Left)
            );

        /// <summary>
        /// Gets or sets whether the <see cref="Expander" /> is opened.
        /// </summary>
        [Category("Common")]
        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
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
        /// Identifies the <see cref="IsOpened"/> dependency property.
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
        public FontFamily HeaderFontFamily
        {
            get => GetValue(HeaderFontFamilyProperty) as FontFamily;
            set => SetValue(HeaderFontFamilyProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderFontFamilyProperty =
            DependencyProperty.Register(
                nameof(HeaderFontFamily),
                typeof(FontFamily),
                TypeofThis,
                new PropertyMetadata(new FontFamily(DefaultHeaderFontName))
            );

        /// <summary>
        /// 
        /// </summary>
        public double HeaderFontSize
        {
            get => (double)GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(
                nameof(HeaderFontSize),
                typeof(double),
                TypeofThis,
                new PropertyMetadata(DefaultHeaderFontSize)
            );

        /// <summary>
        /// 
        /// </summary>
        public FontStretch HeaderFontStretch
        {
            get => (FontStretch)GetValue(HeaderFontStretchProperty);
            set => SetValue(HeaderFontStretchProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HeaderFontStretchProperty =
            DependencyProperty.Register(
                nameof(HeaderFontStretch),
                typeof(FontStretch),
                TypeofThis,
                new PropertyMetadata(FontStretches.Normal)
            );

        /// <summary>
        /// Controls corner radius for Expander Border.
        /// </summary>
        [Category("Appearance")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                TypeofThis,
                new PropertyMetadata(new CornerRadius(0))
            );

        /// <summary>
        /// Controls the duration of the Expander Opening / Closing Animation.
        /// </summary>
        [Category("Behavior")]
        public int AnimationDuration
        {
            get => (int)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="AnimationDuration"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                typeof(int),
                TypeofThis,
                new PropertyMetadata(200)
            );

        /// <summary>
        /// Controls the scale of the Expander Icon.
        /// </summary>
        [Category("Appearance")]
        public double IconZoom
        {
            get => (double)GetValue(IconZoomProperty);
            set => SetValue(IconZoomProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IconZoom"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconZoomProperty =
            DependencyProperty.Register(
                nameof(IconZoom),
                typeof(double),
                TypeofThis,
                new PropertyMetadata(0.7)
            );

        /// <inheritdoc />
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

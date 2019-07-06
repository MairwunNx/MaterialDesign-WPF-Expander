using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesign_WPF_Expander.EventArguments;
using static System.Windows.Application;

namespace MaterialDesign_WPF_Expander
{
    /// <summary>
    /// Base class for <c>Material Design Expander ui-element</c>.
    /// <para/>
    /// This class contains the basic logic for working
    /// with the <c>Expander ui-element</c>.
    /// <para/>
    /// This class is inherited from the <see cref="Control"/> class.
    /// <para/>
    /// This <c>Expander ui-element</c> is not affiliated with
    /// the official Material Design guidelines!
    /// <para/>
    /// This ui-element was created in accordance with
    /// one of the sites on Google https://domains.google/#/.
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
        /// Identifies the <see cref="OpenedEvent"/> event delegate.
        /// </summary>
        /// <param name="sender">expander ui-element class instance</param>
        /// <param name="e">Just empty event arguments</param>
        public delegate void OpenedDelegate(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the expander finished the animation and is fully opened.
        /// </summary>
        public event OpenedDelegate OpenedEvent;

        /// <summary>
        /// Identifies the <see cref="OpenEvent"/> event delegate.
        /// </summary>
        /// <param name="sender">expander ui-element class instance</param>
        /// <param name="e">Just empty event arguments</param>
        public delegate void OpenDelegate(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the expander starting the animation and starts to open.
        /// </summary>
        public event OpenDelegate OpenEvent;

        /// <summary>
        /// Identifies the <see cref="ClosedEvent"/> event delegate.
        /// </summary>
        /// <param name="sender">expander ui-element class instance</param>
        /// <param name="e">Just empty event arguments</param>
        public delegate void ClosedDelegate(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the expander finished the animation and is fully closed.
        /// </summary>
        public event ClosedDelegate ClosedEvent;

        /// <summary>
        /// Identifies the <see cref="CloseEvent"/> event delegate.
        /// </summary>
        /// <param name="sender">expander ui-element class instance</param>
        /// <param name="e">Just empty event arguments</param>
        public delegate void CloseDelegate(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the expander starting the animation and starts to close.
        /// </summary>
        public event CloseDelegate CloseEvent;

        /// <summary>
        /// Identifies the <see cref="IsOpenedChangedEvent"/> event delegate.
        /// </summary>
        /// <param name="sender">expander ui-element class instance</param>
        /// <param name="e">Just empty event arguments</param>
        public delegate void IsOpenedChangedDelegate(
            object sender,
            IsOpenedChangedEventArgs e
        );

        /// <summary>
        /// Occurs when the expander <see cref="IsOpened"/> value changed.
        /// </summary>
        public event IsOpenedChangedDelegate IsOpenedChangedEvent;

        /// <summary>
        /// Set or get the content object that was set in the
        /// tags of the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Common</c>.</para>
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
        /// Set or get the expander title text that was set in
        /// the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Common</c>.</para>
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
        /// Set or get the expander icon visibility that was set in
        /// the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Appearance</c>.</para>
        /// </summary>
        [Category("Appearance")]
        public bool IconIsVisible
        {
            get => (bool) GetValue(IconIsVisibleProperty);
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
        /// Set or get the expander title horizontal alignment
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Layout</c>.</para>
        /// </summary>
        [Category("Layout")]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get => (HorizontalAlignment) GetValue(HeaderHorizontalAlignmentProperty);
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

        private void IsOpenedChanged(DependencyPropertyChangedEventArgs evt)
        {
            var value = (bool) evt.NewValue;

            // We aren't initialized yet, setter was called from parent Window initialization.
            if (_expanderBorder == null)
            {
                // Anyway we don't need any animations to occur in first rendering.
                return;
            }

            Storyboard.SetTargetName(_expanderBorder, _expanderBorder.Name);
            IsOpenedChangedEvent?.Invoke(
                this,
                new IsOpenedChangedEventArgs { IsOpened = value }
            );

            if (value)
            {
                OpenEvent?.Invoke(this, EventArgs.Empty);
                _contentPresenter.Visibility = Visibility.Visible;

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
                storyboard.Completed += (s, e) => { OpenedEvent?.Invoke(this, EventArgs.Empty); };
                storyboard.Begin(_expanderBorder);
            }
            else
            {
                CloseEvent?.Invoke(this, EventArgs.Empty);

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
                    IsOpened = false;
                    _contentPresenter.Visibility = Visibility.Hidden;
                    ClosedEvent?.Invoke(this, EventArgs.Empty);
                };
                storyboard.Begin(_expanderBorder);
            }
        }

        /// <summary>
        /// Set or get the expander is opened state
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Common</c>.</para>
        /// </summary>
        [Category("Common")]
        public bool IsOpened
        {
            get => (bool) GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IsOpened"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register(
                nameof(IsOpened),
                typeof(bool),
                TypeofThis,
                new PropertyMetadata(false, (d, e) => ((Expander) d).IsOpenedChanged(e))
            );

        /// <summary>
        /// Set or get the expander header font family
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Text Appearance</c>.</para>
        /// </summary>
        [Category("Text Appearance")]
        public FontFamily HeaderFontFamily
        {
            get => GetValue(HeaderFontFamilyProperty) as FontFamily;
            set => SetValue(HeaderFontFamilyProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderFontFamily"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontFamilyProperty =
            DependencyProperty.Register(
                nameof(HeaderFontFamily),
                typeof(FontFamily),
                TypeofThis,
                new PropertyMetadata(new FontFamily(DefaultHeaderFontName))
            );

        /// <summary>
        /// Set or get the expander header font size
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Text Appearance</c>.</para>
        /// </summary>
        [Category("Text Appearance")]
        public double HeaderFontSize
        {
            get => (double) GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderFontSize"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(
                nameof(HeaderFontSize),
                typeof(double),
                TypeofThis,
                new PropertyMetadata(DefaultHeaderFontSize)
            );

        /// <summary>
        /// Set or get the expander header font stretch
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Text Appearance</c>.</para>
        /// </summary>
        [Category("Text Appearance")]
        public FontStretch HeaderFontStretch
        {
            get => (FontStretch) GetValue(HeaderFontStretchProperty);
            set => SetValue(HeaderFontStretchProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderFontStretch"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontStretchProperty =
            DependencyProperty.Register(
                nameof(HeaderFontStretch),
                typeof(FontStretch),
                TypeofThis,
                new PropertyMetadata(FontStretches.Normal)
            );

        /// <summary>
        /// Set or get the expander header font style
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Text Appearance</c>.</para>
        /// </summary>
        [Category("Text Appearance")]
        public FontStyle HeaderFontStyle
        {
            get => (FontStyle) GetValue(HeaderFontStyleProperty);
            set => SetValue(HeaderFontStyleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderFontStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontStyleProperty =
            DependencyProperty.Register(
                nameof(HeaderFontStyle),
                typeof(FontStyle),
                TypeofThis,
                new PropertyMetadata(FontStyles.Normal)
            );

        /// <summary>
        /// Set or get the expander header font weight
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Text Appearance</c>.</para>
        /// </summary>
        [Category("Text Appearance")]
        public FontWeight HeaderFontWeight
        {
            get => (FontWeight) GetValue(HeaderFontWeightProperty);
            set => SetValue(HeaderFontWeightProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HeaderFontWeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.Register(
                nameof(HeaderFontWeight),
                typeof(FontWeight),
                TypeofThis,
                new PropertyMetadata(FontWeights.Light)
            );

        /// <summary>
        /// Set or get the expander border corner radius
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Appearance</c>.</para>
        /// </summary>
        [Category("Appearance")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
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
        /// Set or get the expander opening \ closing animation
        /// duration that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Appearance</c>.</para>
        /// </summary>
        [Category("Behavior")]
        public int AnimationDuration
        {
            get => (int) GetValue(AnimationDurationProperty);
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
        /// Set or get the expander header icon zoom
        /// that was set in the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Appearance</c>.</para>
        /// </summary>
        [Category("Appearance")]
        public double IconZoom
        {
            get => (double) GetValue(IconZoomProperty);
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

        /// <summary>
        /// Set or get the expander background color
        /// for the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Brush</c>.</para>
        /// </summary>
        public new Brush Background
        {
            get => GetValue(BackgroundProperty) as Brush;
            set => SetValue(BackgroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Background"/> dependency property.
        /// </summary>
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                nameof(Background),
                typeof(Brush),
                TypeofThis,
                new PropertyMetadata(Current.Resources["Ex-Background-Brush"] as Brush)
            );

        /// <summary>
        /// Set or get the expander header foreground color
        /// for the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Brush</c>.</para>
        /// </summary>
        public new Brush Foreground
        {
            get => GetValue(ForegroundProperty) as Brush;
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Foreground"/> dependency property.
        /// </summary>
        public new static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                nameof(Foreground),
                typeof(Brush),
                TypeofThis,
                new PropertyMetadata(Current.Resources["Ex-HeaderForeground-Brush"] as Brush)
            );

        /// <summary>
        /// Set or get the expander header hover foreground color
        /// for the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Brush</c>.</para>
        /// </summary>
        [Category("Brush")]
        public Brush HoverForeground
        {
            get => GetValue(HoverForegroundProperty) as Brush;
            set => SetValue(HoverForegroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HoverForeground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register(
                nameof(HoverForeground),
                typeof(Brush),
                TypeofThis,
                new PropertyMetadata(Current.Resources["Ex-HeaderHoverForeground-Brush"] as Brush)
            );

        /// <summary>
        /// Set or get the expander border color
        /// for the Expander ui-element.
        /// 
        /// <para>Designer Category: <c>Brush</c>.</para>
        /// </summary>
        public new Brush BorderBrush
        {
            get => GetValue(BorderBrushProperty) as Brush;
            set => SetValue(BorderBrushProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="BorderBrush"/> dependency property.
        /// </summary>
        public new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(
                nameof(BorderBrush),
                typeof(Brush),
                TypeofThis,
                new PropertyMetadata(Current.Resources["Ex-BorderFill-Brush"] as Brush)
            );

        /// <summary>
        /// This overridden method assign the used ui elements
        /// for work Expander-ui element.
        /// <para/>
        /// Also this overridden method initializes some parameters
        /// after loading the composite ui-elements of the Expander.
        /// </summary>
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
                InitExpanderContentByIsOpened();
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

        private void InitExpanderContentByIsOpened()
        {
            if (!IsOpened)
            {
                _contentPresenter.Visibility = Visibility.Hidden;
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

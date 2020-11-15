using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LateralMenu.Model;
using LateralMenu.Service;

namespace LateralMenu.Control
{
    public partial class SideMenu : IMaterialContainer
    {
        private bool _isDisplayed;

        public SideMenu()
        {
            InitializeComponent();
            Children = new List<FrameworkElement>();
            ClosingType = ClosingType.Auto;
        }

        public ClosingType ClosingType { get; set; }

        #region ShadowBackground Dependency property

        public Brush ShadowBackground
        {
            get => (Brush)GetValue(ShadowBackgroundProperty);
            set => SetValue(ShadowBackgroundProperty, value);
        }

        public static readonly DependencyProperty ShadowBackgroundProperty = DependencyProperty.Register("ShadowBackground", typeof(Brush), typeof(SideMenu),
            new FrameworkPropertyMetadata(new SolidColorBrush { Color = Colors.Black, Opacity = .2 }, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region MenuBackground Dependency Property

        public Brush MenuBackground
        {
            get => (Brush) GetValue(MenuBackgroundProperty);
            set => SetValue(MenuBackgroundProperty, value);
        }

        public static readonly DependencyProperty MenuBackgroundProperty = DependencyProperty.Register(
            "MenuBackground", typeof(Brush), typeof(SideMenu), new PropertyMetadata(default(Brush)));

        #endregion

        #region HoverBackground Dependency Property

        public Brush HoverBackground
        {
            get => (Brush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
            "HoverBackground", typeof(Brush), typeof(SideMenu), new PropertyMetadata(default(Brush)));

        #endregion

        #region ArrowBackground Dependency Property

        public Brush ArrowBackground
        {
            get => (Brush)GetValue(ArrowBackgroundProperty);
            set => SetValue(ArrowBackgroundProperty, value);
        }

        public static readonly DependencyProperty ArrowBackgroundProperty = DependencyProperty.Register(
            "ArrowBackground", typeof(Brush), typeof(SideMenu), new PropertyMetadata(default(Brush)));

        #endregion

        #region MenuWidth Dependency property

        public GridLength MenuWidth
        {
            get => (GridLength) GetValue(MenuWidthProperty);
            set => SetValue(MenuWidthProperty, value);
        }

        public static readonly DependencyProperty MenuWidthProperty = DependencyProperty.Register("MenuWidth", typeof(GridLength), typeof(SideMenu));

        #endregion

        #region Children Dependency property

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(
            "Children", typeof(List<FrameworkElement>), typeof(SideMenu), 
            new FrameworkPropertyMetadata(default(List<FrameworkElement>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [Bindable(true)]
        public List<FrameworkElement> Children
        {
            get => (List<FrameworkElement>) GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        #endregion


        public void Toggle()
        {
            Display(!_isDisplayed);
        }

        private void Display(bool display)
        {
            if (display)
                Show();
            else
                Hide();
        }

        public void Show()
        {
            var animation = new DoubleAnimation
            {
                From = -MenuWidth.Value*.85,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            _isDisplayed = true;

            ElementPropagator.Propagate(this, Children);
            WindowSizeEvent();

            var window = Window.GetWindow(this);
            window.SizeChanged += WindowSizeEvent;
        }

        public void Hide()
        {
            var animation = new DoubleAnimation
            {
                To = -MenuWidth.Value,
                Duration = TimeSpan.FromMilliseconds(100)
            };
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            _isDisplayed = false;
            SetShadowWidth(0);
            var window = Window.GetWindow(this);
            window.SizeChanged -= WindowSizeEvent;
        }

        public override void OnApplyTemplate()
        {
            Panel.SetZIndex(this, int.MaxValue);
            RenderTransform = new TranslateTransform(-MenuWidth.Value, 0);
            (FindName("MenuColumn") as ColumnDefinition).Width = MenuWidth;

            // This is a little hack to fire property changes.
            // WPF so complex, it could be much simple...
            ShadowBackground = ShadowBackground;
        }

        private void OnShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClosingType == ClosingType.Auto) Hide();
        }

        public void InternalElementClicked()
        {
            Hide();
        }

        private void WindowSizeEvent(object source, SizeChangedEventArgs args)
        {
            WindowSizeEvent();
        }

        private void WindowSizeEvent()
        {
            var window = Window.GetWindow(this);
            SetShadowWidth(window.ActualWidth - MenuWidth.Value);
        }

        private void SetShadowWidth(double width)
        {
            (FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(width);
        }
    }
}
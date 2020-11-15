using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LateralMenu.Model;

namespace LateralMenu.Control
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : IMaterialItem
    {
        public MenuButton()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Text Dependency property

        [Bindable(true)]
        public string Text
        {
            get
            { var a =(string) GetValue(TextProperty);
                return a;
            }
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MenuButton), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region HoverBackground Dependency property

        public Brush HoverBackground
        {
            get => (Brush) GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(MenuButton), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region Image Dependency property

        public ImageSource Image
        {
            get => (ImageSource) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(MenuButton));

        #endregion

        #region Command Dependency property

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuButton),
            new PropertyMetadata(null));

        #endregion

        #region CommandParameter Dependency property

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(MenuButton),
            new PropertyMetadata(null));

        #endregion

        public IMaterialContainer ParentContainer { get; private set; }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ParentContainer?.InternalElementClicked();
            if (Command?.CanExecute(CommandParameter) == true)
                Command.Execute(CommandParameter);
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
        }

        public void SetParent(IMaterialContainer container)
        {
            ParentContainer = container;
        }
    }
}
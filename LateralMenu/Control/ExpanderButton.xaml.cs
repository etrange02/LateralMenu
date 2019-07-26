using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using LateralMenu.Model;

namespace LateralMenu.Control
{
    /// <summary>
    /// Logique d'interaction pour ExpanderButton.xaml
    /// </summary>
    public partial class ExpanderButton : IMaterialContainer, IMaterialItem, INotifyPropertyChanged
    {
        public ExpanderButton()
        {
            InitializeComponent();
            Children = new List<FrameworkElement>();
            LayoutRoot.DataContext = this;
        }

        public IMaterialContainer ParentContainer { get; private set; }
        
        #region IsExpanded Dependency property

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set
            {
                SetValue(IsExpandedProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpanderButton), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region HoverBackground Dependency property

        public Brush HoverBackground
        {
            get => (Brush) GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(ExpanderButton),
                new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion
        
        #region Image Dependency property

        public ImageSource Image
        {
            get => (ImageSource) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ExpanderButton));

        #endregion

        #region Children Dependency property

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(
            "Children",
            typeof(IList), typeof(ExpanderButton),
            new FrameworkPropertyMetadata(default(List<FrameworkElement>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [Bindable(true)]
        public IList Children
        {
            get => (IList)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        #endregion

        #region ChildrenTemplate

        public static readonly DependencyProperty ChildrenTemplateProperty = DependencyProperty.Register(
            "ChildrenTemplate", 
            typeof(DataTemplate), typeof(ExpanderButton),
            new FrameworkPropertyMetadata(default(DataTemplate), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DataTemplate ChildrenTemplate
        {
            get => (DataTemplate) GetValue(ChildrenTemplateProperty);
            set => SetValue(ChildrenTemplateProperty, value);
        }

        #endregion

        #region Header Dependency Property

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(object), typeof(ExpanderButton), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        #endregion

        #region ArrowColor

        public Brush ArrowColor
        {
            get => (Brush)GetValue(ArrowColorProperty);
            set => SetValue(ArrowColorProperty, value);
        }

        public static readonly DependencyProperty ArrowColorProperty = DependencyProperty.Register(
            "ArrowColor", typeof(Brush), typeof(ExpanderButton), new PropertyMetadata(default(Brush)));

        #endregion

        public void Close()
        {
            IsExpanded = false;
        }

        private void UserControl_MouseDown(object sender, RoutedEventArgs routedEventArgs)
        {
            IsExpanded = !IsExpanded;
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
        }

        public void InternalElementClicked()
        {
            Close();
            ParentContainer.InternalElementClicked();
        }

        public void SetParent(IMaterialContainer container)
        {
            ParentContainer = container;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace DirectoryFileCount.Views.Helpers
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LabelPasswordView : UserControl
    {
        public LabelPasswordView()
        {
            InitializeComponent();
            //DataContext = this;
        }

        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register
        (
            "PropertyValue",
            typeof(string),
            typeof(LabelPasswordView),
            new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
        (
            "PropertyName",
            typeof(string),
            typeof(LabelPasswordView),
            new PropertyMetadata(string.Empty)
        );
        public string PropertyValue
        {
            get { return (string)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }
    }
}

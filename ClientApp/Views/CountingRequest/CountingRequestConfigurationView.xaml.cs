using System.Windows.Controls;

namespace DirectoryFileCount.Views.CountingRequest
{
    /// <summary>
    /// Interaction logic for CountingRequestConfigurationView.xaml
    /// </summary>
    public partial class CountingRequestConfigurationView : UserControl
    {
        public CountingRequestConfigurationView(InterfaceAndModels.Models.CountingRequest countingRequest)
        {
            InitializeComponent();
            DataContext = new CountingRequestConfigurationView(countingRequest);
        }
    }
}

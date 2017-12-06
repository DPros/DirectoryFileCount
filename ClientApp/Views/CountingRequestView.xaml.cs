using System.Windows.Controls;

namespace DirectoryFileCount.Views
{
    /// <summary>
    /// Interaction logic for CountingRequestView.xaml
    /// </summary>
    public partial class CountingRequestView : UserControl
    {
        public CountingRequestView(InterfaceAndModels.Models.CountingRequest countingRequest)
        {
            DataContext = new CountingRequestView(countingRequest);
            InitializeComponent();
        }
    }
}

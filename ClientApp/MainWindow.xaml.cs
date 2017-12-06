using System.Windows;
using System.Windows.Controls;
using FontAwesome.WPF;
using SignInWindow = DirectoryFileCount.Views.Authentication.SignInWindow;

namespace DirectoryFileCount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        private ImageAwesome _loader;

        public MainWindow()
        {
            InitializeComponent();
            ShowSignInWindow();
        }

        private void ShowSignInWindow()
        {
            Visibility = Visibility.Hidden;
            SignInWindow loginWindow = new SignInWindow();
            loginWindow.Closed += (sender, args) => Initialize();
            loginWindow.ShowDialog();
        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            _mainWindowViewModel = new MainWindowViewModel(o =>
            {
                _mainWindowViewModel = null;
                StationManager.CurrentUser = null;
                ShowSignInWindow();
            });
            DataContext = _mainWindowViewModel;
            _mainWindowViewModel.RequestLoader += OnRequestLoader;
        }
        
        private void OnRequestLoader(bool isShow)
        {
            if (isShow && _loader == null)
            {
                _loader = new ImageAwesome();
                MainGrid.Children.Add(_loader);
                _loader.Icon = FontAwesomeIcon.Refresh;
                _loader.Spin = true;
                _loader.Width = 20;
                _loader.Height = 20;
                Grid.SetRow(_loader, 0);
                Grid.SetColumn(_loader, 0);
                Grid.SetColumnSpan(_loader, 3);
                Grid.SetRowSpan(_loader, 3);
                _loader.HorizontalAlignment = HorizontalAlignment.Center;
                _loader.VerticalAlignment = VerticalAlignment.Center;
                IsEnabled = false;
            }
            else if (_loader != null)
            {
                MainGrid.Children.Remove(_loader);
                _loader = null;
                IsEnabled = true;
            }
        }
    }
}
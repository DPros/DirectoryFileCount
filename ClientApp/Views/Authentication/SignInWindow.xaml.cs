using System.Windows;
using System.Windows.Controls;
using DirectoryFileCount.ViewModels.Authentication;
using FontAwesome.WPF;

namespace DirectoryFileCount.Views.Authentication
{
    internal partial class SignInWindow
    {
        #region Fields
        private readonly SignInViewModel _signInViewModel;
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        internal SignInWindow()
        {
            InitializeComponent();
            _signInViewModel = new SignInViewModel();
            _signInViewModel.RequestClose += Close;
            _signInViewModel.RequestLoader += OnRequestLoader;
            _signInViewModel.RequestVisibilityChange += (x) => Visibility = x;
            DataContext = _signInViewModel;
        }
        #endregion

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

        #region EventHandlers
        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _signInViewModel.Password = Password.Password;
        }

        private void Close(bool isQuitApp)
        {
            if (!isQuitApp)
                Close();
            else
                StationManager.ShutDown(0);
        } 
        #endregion
    }
}
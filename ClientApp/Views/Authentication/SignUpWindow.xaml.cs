using System.Windows;
using System.Windows.Controls;
using DirectoryFileCount.ViewModels.Authentication;
using FontAwesome.WPF;

namespace DirectoryFileCount.Views.Authentication
{  
    internal partial class SignUpWindow
    {
        #region Fields
        private readonly SignUpViewModel _signUpViewModel;
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        internal SignUpWindow()
        {
            InitializeComponent();
            _signUpViewModel = new SignUpViewModel();
            _signUpViewModel.RequestClose += Close;
            _signUpViewModel.RequestLoader += OnRequestLoader;
            DataContext = _signUpViewModel;
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
                _loader.Width = _loader.Height = 20;
                Grid.SetRow(_loader, 0);
                Grid.SetColumn(_loader, 0);
                Grid.SetColumnSpan(_loader, 2);
                Grid.SetRowSpan(_loader, 10);
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
            _signUpViewModel.Password = Password.Password;
        }
        
        #endregion
    }
}
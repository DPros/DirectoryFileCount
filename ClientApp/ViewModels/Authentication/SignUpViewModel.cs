using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using DirectoryFileCount.InterfaceAndModels;
using DirectoryFileCount.InterfaceAndModels.Models;
using DirectoryFileCount.Properties;
using DirectoryFileCount.Tools;

namespace DirectoryFileCount.ViewModels.Authentication
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        private string _firstName;
        private string _lastName;
        private string _email; 
        #endregion

        #region Properties
        #region Command
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; } 
        #endregion

        internal string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value; 
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConstructorAndInit
        internal SignUpViewModel()
        {
            InitializeComands();
        }
        
        private void InitializeComands()
        {
            CloseCommand = new RelayCommand(obj => OnRequestClose());
            SignUpCommand = new RelayCommand(SignUp, o => !String.IsNullOrEmpty(_login) &&
                                                          !String.IsNullOrEmpty(_password) &&
                                                          !String.IsNullOrEmpty(_firstName) &&
                                                          !String.IsNullOrEmpty(_lastName) &&
                                                          !String.IsNullOrEmpty(_email));
        }
        #endregion
       
        private async void SignUp(object obj)
        {
            bool isUserCreated = false;
            OnRequestLoader(true);
            await Task.Run(() =>
            {
                try
                {
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show(String.Format(Resources.SignUp_EmailIsNotValid, _email));
                        return;
                    }
                    if (CountingRequestServiceWrapper.UserExists(_login))
                    {
                        MessageBox.Show(String.Format(Resources.SignUp_UserAlreadyExists, _login));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(String.Format(Resources.SignUp_FailedToValidateData, Environment.NewLine, ex.Message), ex);
                    MessageBox.Show(String.Format(Resources.SignUp_FailedToValidateData, Environment.NewLine,
                        ex.Message));
                    return;
                }
                try
                {
                    var user = new User(_firstName, _lastName, _email, _login, _password);
                    CountingRequestServiceWrapper.AddUser(user);
                    isUserCreated = true;
                }
                catch (Exception ex)
                {
                    Logger.Log(String.Format(Resources.SignUp_FailedToCreateUser, Environment.NewLine, ex.Message), ex);
                    MessageBox.Show(String.Format(Resources.SignUp_FailedToCreateUser, Environment.NewLine,
                        ex.Message));
                    return;
                }
                MessageBox.Show(String.Format(Resources.SignUp_UserSuccessfulyCreated, _login));
            });
            OnRequestLoader(false);
            if (isUserCreated)
                OnRequestClose();
        }

        #region EventsAndHandlers
        #region Close
        internal event CloseHandler RequestClose;
        public delegate void CloseHandler();

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }
        #endregion

        #region Loader
        internal event LoaderHandler RequestLoader;
        public delegate void LoaderHandler(bool isShow);

        protected virtual void OnRequestLoader(bool isShow)
        {
            RequestLoader?.Invoke(isShow);
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
        #endregion
    }
}
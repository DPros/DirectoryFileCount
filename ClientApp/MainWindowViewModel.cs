using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectoryFileCount.InterfaceAndModels;
using DirectoryFileCount.InterfaceAndModels.Models;
using DirectoryFileCount.Properties;
using FontAwesome.WPF;

namespace DirectoryFileCount
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private CountingRequest _selectedCountingRequest;
        private readonly ObservableCollection<CountingRequest> _countingRequests;
        private ImageAwesome _loader;
        
        #endregion

        #region Properties

        #region Commands

        public RelayCommand AddCountingRequestCommand => new RelayCommand(AddCountingRequest);

        public RelayCommand LogoutCommand { get; private set; }

        #endregion

        public ObservableCollection<CountingRequest> CountingRequests
        {
            get { return _countingRequests; }
        }

        public CountingRequest SelectedCountingRequest
        {
            get { return _selectedCountingRequest; }
            set
            {
                _selectedCountingRequest = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Action<object> onLogoutAction)
        {
            LogoutCommand = new RelayCommand(onLogoutAction);
            _countingRequests = new ObservableCollection<CountingRequest>();
            StationManager.CurrentUser.CountingRequests.ForEach(request => _countingRequests.Add(request));
            if (_countingRequests.Count > 0)
            {
                _selectedCountingRequest = CountingRequests[0];
            }
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedCountingRequest")
                OnCountingRequestChanged(_selectedCountingRequest);
        }

        #endregion

        private async void AddCountingRequest(object o)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    OnRequestLoader(true);
                    var countingResult = await Task.Run(() =>
                    {
                        var filesCount = Directory.GetFiles(fbd.SelectedPath).Length;
                        var directoriesCount = Directory.GetDirectories(fbd.SelectedPath).Length;
                        var size = GetDirectorySize(fbd.SelectedPath);
                        CountingRequest countingRequest = new CountingRequest(fbd.SelectedPath,
                            StationManager.CurrentUser,
                            directoriesCount, filesCount, size);
                        CountingRequestServiceWrapper.AddCountingRequest(countingRequest);
                        return countingRequest;
                    });
                    _countingRequests.Add(countingResult);
                    _selectedCountingRequest = countingResult;
                    OnPropertyChanged();
                    OnRequestLoader(false);
                }
            }
        }

        internal event LoaderHandler RequestLoader;
        internal delegate void LoaderHandler(bool isShow);

        internal virtual void OnRequestLoader(bool isShow)
        {
            RequestLoader?.Invoke(isShow);
        }

        private static long GetDirectorySize(string parentDir)
        {
            return Directory.EnumerateFiles(parentDir).Sum(s => new FileInfo(s).Length) +
                   Directory.EnumerateDirectories(parentDir).Sum(s => GetDirectorySize(s));
        }

        #region EventsAndHandlers

        #region Loader

        internal event CountingRequestChangedHandler CountingRequest;

        internal delegate void CountingRequestChangedHandler(CountingRequest countingRequest);

        internal virtual void OnCountingRequestChanged(CountingRequest countingRequest)
        {
            CountingRequest?.Invoke(countingRequest);
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
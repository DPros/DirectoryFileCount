using System.Data.SqlTypes;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.ViewModels
{
    class CountingRequestViewModel
    {
        #region Fields

        private readonly CountingRequest _currentCountingRequest;

        #endregion

        #region Properties

        #region Commands

        #endregion

        public string Path
        {
            get { return _currentCountingRequest.Path; }
        }

        public SqlDateTime Date
        {
            get { return _currentCountingRequest.Date; }
        }

        public int DirectoriesCount
        {
            get { return _currentCountingRequest.DirectoriesCount; }
        }

        public int FilesCount
        {
            get { return _currentCountingRequest.FilesCount; }
        }

        public long Size
        {
            get { return _currentCountingRequest.TotalSize; }
        }

        #endregion

        #region Constructor

        public CountingRequestViewModel(CountingRequest countingRequest)
        {
            _currentCountingRequest = countingRequest;
        }

        #endregion
    }
}
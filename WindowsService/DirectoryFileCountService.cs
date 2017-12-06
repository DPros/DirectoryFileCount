using DirectoryFileCount.DBAdapter;
using DirectoryFileCount.InterfaceAndModels;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.Service
{

    class DirectoryFileCountService: IDirectoryFileCountService
    {
        public bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        public void AddCountingRequest(CountingRequest countingRequest)
        {
            EntityWrapper.AddCountingRequest(countingRequest);
        }

        public void DeleteCountingRequest(CountingRequest countingRequest)
        {
            EntityWrapper.DeleteCountingRequest(countingRequest);
        }
    }
}

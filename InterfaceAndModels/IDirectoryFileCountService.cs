using System.ServiceModel;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.InterfaceAndModels
{
    [ServiceContract]
    public interface  IDirectoryFileCountService
    {
        [OperationContract]
        bool UserExists(string login);
        [OperationContract]
        User GetUserByLogin(string login);
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void AddCountingRequest(CountingRequest countingRequest);
        [OperationContract]
        void DeleteCountingRequest(CountingRequest countingRequest);

    }
}

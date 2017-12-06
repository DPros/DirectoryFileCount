using System.ServiceModel;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.InterfaceAndModels
{
    public class CountingRequestServiceWrapper
    {
        public static bool UserExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IDirectoryFileCountService>("Server"))
            {
                IDirectoryFileCountService client = myChannelFactory.CreateChannel();
                return client.UserExists(login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IDirectoryFileCountService>("Server"))
            {
                IDirectoryFileCountService client = myChannelFactory.CreateChannel();
                return client.GetUserByLogin(login);
            }
        }

        public static void AddUser(User user)
        {
            using (var myChannelFactory = new ChannelFactory<IDirectoryFileCountService>("Server"))
            {
                IDirectoryFileCountService client = myChannelFactory.CreateChannel();
                client.AddUser(user);
            }
        }

        public static void AddCountingRequest(CountingRequest countingRequest)
        {
            using (var myChannelFactory = new ChannelFactory<IDirectoryFileCountService>("Server"))
            {
                IDirectoryFileCountService client = myChannelFactory.CreateChannel();
                client.AddCountingRequest(countingRequest);
            }
        }

        public static void DeleteCountingRequest(CountingRequest countingRequest)
        {
            using (var myChannelFactory = new ChannelFactory<IDirectoryFileCountService>("Server"))
            {
                IDirectoryFileCountService client = myChannelFactory.CreateChannel();
                client.DeleteCountingRequest(countingRequest);
            }
        }
    }
}
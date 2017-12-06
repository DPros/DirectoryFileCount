using System.Linq;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.DBAdapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            using (var context = new CountingRequestContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new CountingRequestContext())
            {
                return context.Users.Include("CountingRequests").FirstOrDefault(u => u.Login == login);
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new CountingRequestContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void AddCountingRequest(CountingRequest countingRequest)
        {
            using (var context = new CountingRequestContext())
            {
                countingRequest.DeleteDatabaseValues();
                context.CountingRequests.Add(countingRequest);
                context.SaveChanges();
            }
        }


        public static void DeleteCountingRequest(CountingRequest countingRequest)
        {
            using (var context = new CountingRequestContext())
            {
                countingRequest.DeleteDatabaseValues();
                context.CountingRequests.Remove(countingRequest);
                context.SaveChanges();
            }
        }

    }
}

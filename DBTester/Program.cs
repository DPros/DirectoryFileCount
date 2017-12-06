using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletSimulator.DBAdapter;

namespace DBTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new WalletContext())
            {
                var users = context.Users.ToList();
                Console.WriteLine(users.Count);
                Console.ReadKey();
            }
        }
    }
}

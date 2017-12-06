using System.Data.Entity;
using DirectoryFileCount.DBAdapter.Migrations;
using DirectoryFileCount.InterfaceAndModels.Models;

namespace DirectoryFileCount.DBAdapter
{
    public class CountingRequestContext:DbContext
    {
        public CountingRequestContext():base("DB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CountingRequestContext, Configuration>("DB"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CountingRequest> CountingRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new CountingRequest.CountingRequestEntityConfiguration());
        }
    }
}

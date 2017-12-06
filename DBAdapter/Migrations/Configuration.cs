using System.Data.Entity.Migrations;

namespace DirectoryFileCount.DBAdapter.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CountingRequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DBAdapter.CountingRequestContext";
        }

        protected override void Seed(CountingRequestContext context)
        {
        }
    }
}

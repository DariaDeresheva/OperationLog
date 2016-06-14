using OperationLog.Database.DatabaseContext;

namespace OperationLog.Database.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<OperationDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OperationLog.Database.DatabaseContext.OperationDatabaseContext";
        }

        protected override void Seed(OperationDatabaseContext context)
        {
        }
    }
}
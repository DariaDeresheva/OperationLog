using System.Data.Entity;

namespace OperationLog.Database.DatabaseContext
{
    public class OperationDatabaseInitializer : DropCreateDatabaseIfModelChanges<OperationDatabaseContext>
    {
        protected override void Seed(OperationDatabaseContext database)
        {
            database.SaveChanges();
        }
    }
}

using System.Data.Entity;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.DatabaseContext
{
    public class OperationDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        static OperationDatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new OperationDatabaseInitializer());
        }

        public OperationDatabaseContext()
        {
        }

        public OperationDatabaseContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using System.Data.Entity;
using System.Reflection;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.DatabaseContext
{
    public class OperationDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Program> Programs { get; set; }

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
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

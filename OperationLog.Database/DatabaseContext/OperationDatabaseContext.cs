using System.Data.Entity;
using System.Reflection;
using OperationLog.Database.Migrations;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.DatabaseContext
{
    /// <summary>
    /// Класс определяет контекст подключения к базе данных.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class OperationDatabaseContext : DbContext
    {
        /// <summary>
        /// Таблица пользователей.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Таблица типов пользователей.
        /// </summary>
        public DbSet<UserType> UserTypes { get; set; }
        /// <summary>
        /// Таблица операций.
        /// </summary>
        public DbSet<Operation> Operations { get; set; }
        /// <summary>
        /// Таблица типов операций.
        /// </summary>
        public DbSet<OperationType> OperationTypes { get; set; }
        /// <summary>
        /// Таблица филиалов.
        /// </summary>
        public DbSet<Department> Departments { get; set; }
        /// <summary>
        /// Таблица программ.
        /// </summary>
        public DbSet<Program> Programs { get; set; }

        /// <summary>
        /// Статический конструктор <see cref="OperationDatabaseContext"/>. Инициализирует подключение с миграцией базы данных к последней версии.
        /// </summary>
        static OperationDatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<OperationDatabaseContext, Configuration>());
            var provider = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        /// <summary>
        /// Этот метод вызывается во время инициализации контекста. 
        /// Применение конфигураций для сущностей при создании базы данных.
        /// </summary>
        /// <param name="modelBuilder">Билдер, который определяет модель для создаваемого контекста.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
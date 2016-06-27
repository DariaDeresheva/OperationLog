using OperationLog.Database.DatabaseContext;

namespace OperationLog.Database.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Класс является конфигурацией миграций базы данных.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{OperationDatabaseContext}" />
    public sealed class Configuration : DbMigrationsConfiguration<OperationDatabaseContext>
    {
        /// <summary>
        /// Конструктор <see cref="Configuration"/>. Конфигурирование миграций базы данных.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OperationLog.Database.DatabaseContext.OperationDatabaseContext";
        }
    }
}
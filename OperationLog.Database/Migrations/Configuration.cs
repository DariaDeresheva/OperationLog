using OperationLog.Database.DatabaseContext;

namespace OperationLog.Database.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// ����� �������� ������������� �������� ���� ������.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{OperationDatabaseContext}" />
    public sealed class Configuration : DbMigrationsConfiguration<OperationDatabaseContext>
    {
        /// <summary>
        /// ����������� <see cref="Configuration"/>. ���������������� �������� ���� ������.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OperationLog.Database.DatabaseContext.OperationDatabaseContext";
        }
    }
}
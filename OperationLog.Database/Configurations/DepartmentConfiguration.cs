using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей филиалов.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Department}" />
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        /// <summary>
        /// Конструктор <see cref="DepartmentConfiguration" />. Конфигурирование сущности <see cref="Department" />.
        /// </summary>
        public DepartmentConfiguration()
        {
            HasMany(department => department.Operations)
                .WithRequired(operation => operation.Department)
                .WillCascadeOnDelete(false);

            Property(department => department.DepartmentName)
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}

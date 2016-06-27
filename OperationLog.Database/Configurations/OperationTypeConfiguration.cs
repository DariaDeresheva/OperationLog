using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей типов операций.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{OperationType}" />
    public class OperationTypeConfiguration : EntityTypeConfiguration<OperationType>
    {
        /// <summary>
        /// Конструктор <see cref="OperationTypeConfiguration" />. Конфигурирование сущности <see cref="OperationType" />.
        /// </summary>
        public OperationTypeConfiguration()
        {
            HasMany(operationType => operationType.Operations)
                .WithRequired(operation => operation.OperationType)
                .WillCascadeOnDelete(false);

            Property(operationType => operationType.TypeName)
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}

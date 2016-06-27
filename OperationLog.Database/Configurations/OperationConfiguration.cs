using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей операций.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Operation}" />
    public class OperationConfiguration : EntityTypeConfiguration<Operation>
    {
        /// <summary>
        /// Конструктор <see cref="OperationConfiguration" />. Конфигурирование сущности <see cref="Operation" />.
        /// </summary>
        public OperationConfiguration()
        {
            Property(operation => operation.TableName)
                .HasColumnType("varchar")
                .HasMaxLength(21);

            Property(operation => operation.StationAddress)
                .HasColumnType("char")
                .HasMaxLength(6);
        }
    }
}

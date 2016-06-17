using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class OperationConfiguration : EntityTypeConfiguration<Operation>
    {
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

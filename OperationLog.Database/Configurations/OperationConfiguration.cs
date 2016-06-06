using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class OperationConfiguration : EntityTypeConfiguration<Operation>
    {
        public OperationConfiguration()
        {
            Property(operation => operation.TableName)
                .HasColumnType("char")
                .HasMaxLength(21)
                .IsFixedLength()
                .IsUnicode(false);

            Property(operation => operation.TableDescription)
                .HasColumnType("char")
                .HasMaxLength(50)
                .IsFixedLength()
                .IsUnicode(false);

            Property(operation => operation.StationAddress)
                .HasColumnType("char")
                .HasMaxLength(6)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}

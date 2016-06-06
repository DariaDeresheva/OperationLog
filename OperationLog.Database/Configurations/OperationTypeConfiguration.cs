using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class OperationTypeConfiguration : EntityTypeConfiguration<OperationType>
    {
        public OperationTypeConfiguration()
        {
            HasMany(operationType => operationType.Operations)
                .WithRequired(operation => operation.OperationType)
                .WillCascadeOnDelete(false);

            Property(operationType => operationType.TypeName)
                .HasColumnType("char")
                .HasMaxLength(30)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}

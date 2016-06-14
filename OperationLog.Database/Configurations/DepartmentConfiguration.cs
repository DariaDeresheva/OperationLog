using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            HasMany(department => department.Operations)
                .WithRequired(operation => operation.Department)
                .WillCascadeOnDelete(false);

            Property(department => department.DepartmentName)
                .HasColumnType("char")
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}

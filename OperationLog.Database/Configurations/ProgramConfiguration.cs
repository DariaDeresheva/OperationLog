using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class ProgramConfiguration : EntityTypeConfiguration<Program>
    {
        public ProgramConfiguration()
        {
            HasMany(program => program.Operations)
                .WithRequired(operation => operation.Program)
                .WillCascadeOnDelete(false);

            Property(program => program.ProgramId)
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsUnicode(false);

            Property(program => program.ProgramName)
                .HasColumnType("char")
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}

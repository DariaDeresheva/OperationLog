using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей программ.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Program}" />
    public class ProgramConfiguration : EntityTypeConfiguration<Program>
    {
        /// <summary>
        /// Конструктор <see cref="ProgramConfiguration" />. Конфигурирование сущности <see cref="Program" />.
        /// </summary>
        public ProgramConfiguration()
        {
            HasMany(program => program.Operations)
                .WithRequired(operation => operation.Program)
                .WillCascadeOnDelete(false);

            Property(program => program.ProgramId)
                .HasColumnType("char")
                .HasMaxLength(1);

            Property(program => program.ProgramName)
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}

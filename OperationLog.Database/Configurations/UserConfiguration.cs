using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей пользователей.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{User}" />
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Конструктор <see cref="UserConfiguration" />. Конфигурирование сущности <see cref="User" />.
        /// </summary>
        public UserConfiguration()
        {
            HasMany(user => user.Operations)
                .WithRequired(operation => operation.User)
                .WillCascadeOnDelete(false);

            Property(user => user.UserName)
                .HasColumnType("varchar")
                .HasMaxLength(36);
        }
    }
}
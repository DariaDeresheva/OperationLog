using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    /// <summary>
    /// Класс является конфигурацией сущностей типов пользователей.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{UserType}" />
    public class UserTypeConfiguration : EntityTypeConfiguration<UserType>
    {
        /// <summary>
        /// Конструктор <see cref="UserTypeConfiguration" />. Конфигурирование сущности <see cref="UserType" />.
        /// </summary>
        public UserTypeConfiguration()
        {
            HasMany(userType => userType.Users)
                .WithRequired(user => user.UserType)
                .WillCascadeOnDelete(false);

            Property(userType => userType.UserTypeId)
                .HasColumnType("char")
                .HasMaxLength(1);

            Property(userType => userType.TypeName)
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}
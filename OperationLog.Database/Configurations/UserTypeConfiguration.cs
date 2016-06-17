using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class UserTypeConfiguration : EntityTypeConfiguration<UserType>
    {
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
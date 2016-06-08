using System.Data.Entity.ModelConfiguration;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Database.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(user => user.Operations)
                .WithRequired(operation => operation.User)
                .WillCascadeOnDelete(false);

            Property(user => user.UserName)
                .HasColumnType("char")
                .HasMaxLength(36)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
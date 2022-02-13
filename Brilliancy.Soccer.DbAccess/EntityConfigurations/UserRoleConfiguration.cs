using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleDbModel>
    {
        public void Configure(EntityTypeBuilder<UserRoleDbModel> modelBuilder)
        {
            modelBuilder.HasKey(bc => new { bc.UserId, bc.RoleId });
            modelBuilder
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.RoleId);
        }
    }
}

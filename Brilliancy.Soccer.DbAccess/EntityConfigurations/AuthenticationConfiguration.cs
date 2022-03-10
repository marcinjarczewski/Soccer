using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class AuthenticationConfiguration : IEntityTypeConfiguration<AuthenticationDbModel>
    {
        public void Configure(EntityTypeBuilder<AuthenticationDbModel> modelBuilder)
        {
                 modelBuilder
                 .HasOne(t => t.CreatedByUser)
                 .WithMany(t => t.Authentications)
                 .HasForeignKey(t => t.CreatedByUserId)
                 .IsRequired();

                modelBuilder
                 .HasOne(t => t.Type)
                 .WithMany(t => t.Authentications)
                 .HasForeignKey(t => t.TypeId)
                 .IsRequired();
        }
    }
}

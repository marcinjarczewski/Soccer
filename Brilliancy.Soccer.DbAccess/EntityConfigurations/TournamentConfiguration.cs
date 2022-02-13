using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<TournamentDbModel>
    {
        public void Configure(EntityTypeBuilder<TournamentDbModel> modelBuilder)
        {
            modelBuilder
                .HasOne(t => t.Owner)
                .WithMany(t => t.OwnedTournaments)
                .HasForeignKey(t => t.OwnerId)
                .IsRequired();

            modelBuilder
                .HasMany(t => t.Admins)
                .WithMany(t => t.TournamentAdmins)
                .UsingEntity(x => x.ToTable("TournamentAdmins"));

            modelBuilder
                .HasMany(t => t.Admins)
                .WithMany(t => t.TournamentAdmins)
                .UsingEntity(t => t.Property("TournamentAdminsId").HasColumnName("TournamentId"));

            modelBuilder
                .HasMany(t => t.Admins)
                .WithMany(t => t.TournamentAdmins)
                .UsingEntity(t => t.Property("AdminsId").HasColumnName("UserId"));

            modelBuilder.HasCheckConstraint("CK_DefaultDayOfTheWeek", "[DefaultDayOfTheWeek] >= 1 AND [DefaultDayOfTheWeek] <= 7");
        }
    }
}

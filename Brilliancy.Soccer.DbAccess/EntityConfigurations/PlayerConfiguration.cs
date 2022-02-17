using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerDbModel>
    {
        public void Configure(EntityTypeBuilder<PlayerDbModel> modelBuilder)
        {
            modelBuilder.HasCheckConstraint("CK_AnyName", "ISNULL([NickName], '') != '' OR ISNULL([FirstName], '') != '' OR ISNULL([LastName], '') != ''");

            modelBuilder
                .HasMany(t => t.Teams)
                .WithMany(t => t.Players)
                .UsingEntity(x => x.ToTable("TeamPlayers"));

            modelBuilder
                .HasMany(t => t.Teams)
                .WithMany(t => t.Players)
                .UsingEntity(t => t.Property("TeamsId").HasColumnName("TeamId"));

            modelBuilder
                .HasMany(t => t.Teams)
                .WithMany(t => t.Players)
                .UsingEntity(t => t.Property("PlayersId").HasColumnName("PlayerId"));
        }
    }
}

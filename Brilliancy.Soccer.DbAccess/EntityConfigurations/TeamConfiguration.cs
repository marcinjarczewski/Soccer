using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<TeamDbModel>
    {
        public void Configure(EntityTypeBuilder<TeamDbModel> modelBuilder)
        {
        }
    }
}

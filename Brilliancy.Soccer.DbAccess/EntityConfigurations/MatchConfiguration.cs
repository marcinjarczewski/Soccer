using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<MatchDbModel>
    {
        public void Configure(EntityTypeBuilder<MatchDbModel> modelBuilder)
        {

        }
    }
}

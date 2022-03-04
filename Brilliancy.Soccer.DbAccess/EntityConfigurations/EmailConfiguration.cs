using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<EmailDbModel>
    {
        public void Configure(EntityTypeBuilder<EmailDbModel> modelBuilder)
        {
        }
    }
}

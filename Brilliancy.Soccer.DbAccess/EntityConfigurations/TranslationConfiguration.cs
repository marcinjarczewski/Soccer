using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<TranslationDbModel>
    {
        public void Configure(EntityTypeBuilder<TranslationDbModel> modelBuilder)
        {
            modelBuilder.HasData(new TranslationDbModel
            {
                Id = 1,
            });

            modelBuilder.HasData(new TranslationDbModel
            {
                Id = 2
            });
        }
    }
}

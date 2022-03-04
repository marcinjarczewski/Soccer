using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<LanguageDbModel>
    {
        public void Configure(EntityTypeBuilder<LanguageDbModel> modelBuilder)
        {
            modelBuilder.HasData(
                new LanguageDbModel
                {
                    Id = (int)LanguageEnum.Polish,
                    Name = LanguageEnum.Polish.ToString()
                },
                new LanguageDbModel
                {
                    Id = (int)LanguageEnum.English,
                    Name = LanguageEnum.English.ToString()
                });
        }
    }
}

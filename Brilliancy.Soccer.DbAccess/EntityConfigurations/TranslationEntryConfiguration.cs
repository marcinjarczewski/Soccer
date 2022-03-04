using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class TranslationEntryConfiguration : IEntityTypeConfiguration<TranslationEntryDbModel>
    {
        public void Configure(EntityTypeBuilder<TranslationEntryDbModel> modelBuilder)
        {
            modelBuilder
              .HasOne(t => t.Translation)
              .WithMany(t => t.TranslationEntries)
              .HasForeignKey(t => t.TranslationId)
              .IsRequired();

            modelBuilder
             .HasOne(t => t.Language)
             .WithMany(t => t.TranslationEntries)
             .HasForeignKey(t => t.TranslationId)
             .IsRequired();

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 1,
                LanguageId = 2,
                Text = ResourceHelper.GetResourceAsString("UserRegisterContentEN.html", typeof(UserDbModel)),
                TranslationId = 1
            });

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 2,
                LanguageId = 2,
                Text = "@Model.Name - welcome to Soccer portal!",
                TranslationId = 2
            });
        }
    }
}

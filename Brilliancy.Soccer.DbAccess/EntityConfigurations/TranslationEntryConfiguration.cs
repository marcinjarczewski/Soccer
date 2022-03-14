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
             .HasForeignKey(t => t.LanguageId)
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

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 3,
                LanguageId = 2,
                Text = ResourceHelper.GetResourceAsString("AdminInvateEN.html", typeof(UserDbModel)),
                TranslationId = 3
            });

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 4,
                LanguageId = 2,
                Text = "@Model.Name, you have been invited to a tournament!",
                TranslationId = 4
            });

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 5,
                LanguageId = 2,
                Text = ResourceHelper.GetResourceAsString("PlayerInvateEN.html", typeof(UserDbModel)),
                TranslationId = 5
            });

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 6,
                LanguageId = 2,
                Text = "@Model.Name, you have been invited to a tournament!",
                TranslationId = 6
            });


            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 7,
                LanguageId = 2,
                Text = ResourceHelper.GetResourceAsString("ForgottenPasswordEN.html", typeof(UserDbModel)),
                TranslationId = 7
            });

            modelBuilder.HasData(new TranslationEntryDbModel
            {
                Id = 8,
                LanguageId = 2,
                Text = "@Model.Name - lost password",
                TranslationId = 8
            });
        }
    }
}

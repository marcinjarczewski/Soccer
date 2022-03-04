using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<TemplateDbModel>
    {
        public void Configure(EntityTypeBuilder<TemplateDbModel> modelBuilder)
        {
            modelBuilder
              .HasOne(t => t.TranslateContent)
              .WithMany(t => t.TemplateContentEntries)
              .HasForeignKey(t => t.TranslateContentId);

            modelBuilder
             .HasOne(t => t.TranslateHeader)
             .WithMany(t => t.TemplateHeaderEntries)
             .HasForeignKey(t => t.TranslateHeaderId);

            modelBuilder.HasData(
                   new TemplateDbModel
                   {
                       Id = (int)TemplateEnum.UserRegister,
                       Header = "@Model.Name - witaj na portalu Soccer",
                       Content = ResourceHelper.GetResourceAsString("UserRegisterContentPL.html", typeof(UserDbModel)),
                       TranslateContentId = 1,
                       TranslateHeaderId = 2
                   });
        }
    }
}

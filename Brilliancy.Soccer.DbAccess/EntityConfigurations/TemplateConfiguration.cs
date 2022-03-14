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

            modelBuilder.HasData(
            new TemplateDbModel
            {
                Id = (int)TemplateEnum.AdminInvite,
                Header = "@Model.Name - potrzebna Twoja pomoc w zarządzaniu turniejem!",
                Content = ResourceHelper.GetResourceAsString("UserRegisterContentPL.html", typeof(UserDbModel)),
                TranslateContentId = 3,
                TranslateHeaderId = 4
            });

            modelBuilder.HasData(
            new TemplateDbModel
            {
                Id = (int)TemplateEnum.PlayerInvite,
                Header = "@Model.Name - zostałeś zaproszony jako grajek!",
                Content = ResourceHelper.GetResourceAsString("UserRegisterContentPL.html", typeof(UserDbModel)),
                TranslateContentId = 5,
                TranslateHeaderId = 6
            });

            modelBuilder.HasData(
              new TemplateDbModel
              {
                  Id = (int)TemplateEnum.ForgottenPassword,
                  Header = "@Model.Name - zmiana hasła",
                  Content = ResourceHelper.GetResourceAsString("ForgottenPasswordPL.html", typeof(UserDbModel)),
                  TranslateContentId = 7,
                  TranslateHeaderId = 8
              });
        }
    }
}

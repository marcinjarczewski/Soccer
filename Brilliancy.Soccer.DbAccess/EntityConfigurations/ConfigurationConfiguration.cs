using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brilliancy.Soccer.DbAccess.EntityConfigurations
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<ConfigurationDbModel>
    {
        public void Configure(EntityTypeBuilder<ConfigurationDbModel> modelBuilder)
        {
            modelBuilder.HasKey(bc => bc.Key);
            modelBuilder.HasData(
                 new ConfigurationDbModel
                 {
                     Key = ConfigurationDictionary.FtpDownloadRoot,
                     Value = "ftp download url",
                     Description = "ftp download url"
                 }, new ConfigurationDbModel
                 {
                     Key = ConfigurationDictionary.FtpUploadRoot,
                     Value = "ftp upload url",
                     Description = "ftp upload url"
                 }, new ConfigurationDbModel
                 {
                     Key = ConfigurationDictionary.FtpPhotoSubfolder,
                     Value = "img/",
                     Description = "ftp subfolder for images",
                 }, new ConfigurationDbModel
                 {
                     Key = ConfigurationDictionary.FtpUser,
                     Value = "ftp login",
                     Description = "ftp_login"
                 }, new ConfigurationDbModel
                 {
                     Key = ConfigurationDictionary.FtpPassword,
                     Value = "ftp password",
                     Description = "password"
                 });

            modelBuilder.HasData(
             new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterAddress,
                 Value = "soccer-noreply@brilliancy.pl",
                 Description = "Sender (no reply) email address"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterName,
                 Value = "Soccer team",
                 Description = "Email name shown to the user"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterPassword,
                 Value = "xyz",
                 Description = "Password to the sender email",
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterPort,
                 Value = "587",
                 Description = "SMTP email port"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterReplyTo,
                 Value = "soccer-noreply@brilliancy.pl",
                 Description = "Email shown as reply to"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterSMTP,
                 Value = "smtp.webio.pl",
                 Description = "Smtp address"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailRegisterSSLEnabled,
                 Value = "true",
                 Description = "Is SMTP use SSL"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailServiceSendingTime,
                 Value = "1;2;5;15;40;120",
                 Description = "Time in minutes separated by ; between unsuccessful emails sent"
             }, new ConfigurationDbModel
             {
                 Key = ConfigurationDictionary.EmailServiceSleepTime,
                 Value = "60000",
                 Description = "Service sleep time"
             });

            modelBuilder.HasData(
                new ConfigurationDbModel
                {
                    Key = ConfigurationDictionary.InvitePlayerDaysExpiration,
                    Value = "5",
                    Description = "Invite players link valid in days"
                },
                new ConfigurationDbModel
                {
                    Key = ConfigurationDictionary.InviteAdminDaysExpiration,
                    Value = "5",
                    Description = "Invite admins link valid in days"
                },
                new ConfigurationDbModel
                {
                    Key = ConfigurationDictionary.ResetPasswordDaysExpiration,
                    Value = "2",
                    Description = "reset password link valid in days"
                });
        }
    }
}

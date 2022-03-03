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
        }
    }
}

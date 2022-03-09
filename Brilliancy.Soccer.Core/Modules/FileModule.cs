using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Providers;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Providers;
using Brilliancy.Soccer.Core.Helpers;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using System;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class FileModule : BaseModule, IFileModule
    {
        private SoccerDbContext _dbContext { get; }

        private IFtpClientFactory  _ftpfactory { get; }
        public FileModule(IMapper mapper, SoccerDbContext context, IFtpClientFactory ftpFactory) : base(mapper)
        {
            _dbContext = context;
            _ftpfactory = ftpFactory;
        }

        public FileDto AddPhoto(MemoryStream file, string fileExtension)
        {
            var img = ImageHelper.Convert(file, 600, 400, false);
            var configs = _dbContext.Configurations.ToList();
            var ftpClient = _ftpfactory.CreateFtpClient(new ConfigurationDto
            {
                FTP_DownloadDirRoot = configs.FirstOrDefault(c => c.Key == ConfigurationDictionary.FtpDownloadRoot)?.Value,
                FTP_Login = configs.FirstOrDefault(c => c.Key == ConfigurationDictionary.FtpUser)?.Value,
                FTP_Password = configs.FirstOrDefault(c => c.Key == ConfigurationDictionary.FtpPassword)?.Value,
                FTP_SubfolderForImages = configs.FirstOrDefault(c => c.Key == ConfigurationDictionary.FtpPhotoSubfolder)?.Value,
                FTP_UploadDirRoot = configs.FirstOrDefault(c => c.Key == ConfigurationDictionary.FtpUploadRoot)?.Value,
            });
            var model = new FileDbModel
            {
                Url = ftpClient.UploadFile(img, $"logo_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}{fileExtension}")
            };
            _dbContext.Files.Add(model);
            _dbContext.SaveChanges();
            return _mapper.Map<FileDto>(model);
        }
    }
}

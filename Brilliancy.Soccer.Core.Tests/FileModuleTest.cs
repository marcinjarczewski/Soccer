using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Providers;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Services;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class FileModuleTest
    {
        private FileModule _fileModule;
        private SoccerDbContext _soccerDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SoccerDbContext>()
                  .UseInMemoryDatabase(databaseName: "SoccerDatabase")
                  .Options;

            // Insert seed data into the database using one instance of the context
            _soccerDbContext = new SoccerDbContext(options);
            _soccerDbContext.Database.EnsureDeleted();

            _soccerDbContext.SaveChanges(); 

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();
            var ftpFactoryMock = new Mock<IFtpClientFactory>();
            var ftpmock = new Mock<IFtpClient>();
            ftpmock.Setup(f => f.UploadFile(It.IsAny<MemoryStream>(), It.IsAny<string>())).Returns("link");
            ftpFactoryMock.Setup(f => f.CreateFtpClient(It.IsAny<ConfigurationDto>())).Returns(ftpmock.Object);
            _fileModule = new FileModule(mapper, _soccerDbContext, ftpFactoryMock.Object);
            var service = new EmailSenderService(null, null);
        }

        [Test]
        public void AddPhoto_Success()
        {
            var filesCount = _soccerDbContext.Files.Count();
            var bitmap = new Bitmap(1000, 800);
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            var file = _fileModule.AddPhoto(memoryStream, ".jpg");
            Assert.AreEqual(filesCount + 1, _soccerDbContext.Files.Count());
            Assert.AreEqual("link", file.Url);
        }

        //[Test]
        //public void Update_Null()
        //{
        //    var ex = Assert.Throws<UserDataException>(() => _emailModule.Update(null));
        //    Assert.IsTrue(ex.Message == CoreTranslations.Email_NoEmail);
        //}
    }
}
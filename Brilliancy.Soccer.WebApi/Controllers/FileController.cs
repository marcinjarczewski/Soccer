using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models.Player.Write;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class FileController : BaseController
    {
        private readonly IFileModule _fileModule;
        public FileController(IMapper mapper, ILoginModule loginModule, IFileModule fileModule) : base(mapper, loginModule)
        {
            _fileModule = fileModule;
        }


        [Route("tournamentLogo")]
        public IActionResult TournamentLogo(IFormFile file)
        {
            if (file == null || file.ContentType == null || !file.ContentType.StartsWith("image"))
            {
                return BadRequest(WebApiTranslations.FileController_IncorrectFormat);
            }
            try
            {
                if (file.Length <= 0)
                {
                    return BadRequest(WebApiTranslations.FileController_FileEmpty);
                }

                var imageStream = new MemoryStream();
                file.CopyTo(imageStream);
                var fileDto = _fileModule.AddPhoto(imageStream,file.FileName.Substring(file.FileName.LastIndexOf('.')));
                return new JsonResult(new BaseResultWithDataReadModel
                {
                    IsSuccess = true,
                    Data = fileDto,
                    Message = WebApiTranslations.FileController_FileSuccess
                });
            }
            catch(Exception ex)
            {
                return new JsonResult(new BaseResultWithDataReadModel
                {
                    IsSuccess = false,
                    Message = WebApiTranslations.FileController_FileError 
                });
            }
        }
    }
}

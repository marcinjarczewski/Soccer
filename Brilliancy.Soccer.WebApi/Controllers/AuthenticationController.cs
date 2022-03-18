using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models.Match.Read;
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
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationModule _authModule;
        public AuthenticationController(IMapper mapper, ILoginModule loginModule, IAuthenticationModule authModule, IHttpContextAccessor httpContextAccessor)
            : base(mapper, loginModule, httpContextAccessor)
        {
            _authModule = authModule;
        }


        [Route("InvitePlayers/{key}")]
        [Authorize]
        public IActionResult InvitePlayers(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true };
            try
            {
                _authModule.ConfirmPlayerInvitation(key, this._CurrentUserInfo.Id);
            }
            catch(Exception ex)
            {
                if(ex is UserDataException || ex is Common.Exceptions.InvalidDataException)
                {
                    model.IsKeyValid = false;
                    model.Message = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }

            model.Message = WebApiTranslations.AuthenticationController_ValidKeyInvitePlayer;
            return View(model);
        }

        [Route("InviteAdmin/{key}")]
        [Authorize]
        public IActionResult InviteAdmin(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true };
            try
            {
                _authModule.ConfirmAdminInvitation(key, this._CurrentUserInfo.Id);
            }
            catch (Exception ex)
            {
                if (ex is UserDataException || ex is Common.Exceptions.InvalidDataException)
                {
                    model.IsKeyValid = false;
                    model.Message = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }

            model.Message = WebApiTranslations.AuthenticationController_ValidKeyInvitePlayer;
            return View(model);
        }

        [Route("LostPassword/{key}")]
        public IActionResult LostPassword(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true };
            try
            {
                _authModule.ConfirmEmailReset(key);
            }
            catch (Exception ex)
            {
                if (ex is UserDataException || ex is Common.Exceptions.InvalidDataException)
                {
                    model.IsKeyValid = false;
                    model.Message = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }

            model.Message = WebApiTranslations.AuthenticationController_ValidKeyInvitePlayer;
            return View(model);
        }
    }
}

using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.WebApi.Models.Authentication.Read;
using Brilliancy.Soccer.WebApi.Models.Authentication.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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


        [Route("/InvitePlayers/{key}")]
        [Authorize]
        public IActionResult InvitePlayers(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true};
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

        [Route("SendInvitationPlayer")]
        [Authorize]
        public IActionResult SendInvitationPlayer(AuthenticationWriteModel model)
        {
            var dto = _mapper.Map<AuthenticationDto>(model);
            _authModule.InvitePlayer(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.AuthenticationController_InvitePlayer
            });
        }

        [Route("SendInvitationAdmin")]
        [Authorize]
        public IActionResult SendInvitationAdmin(AuthenticationWriteModel model)
        {
            var dto = _mapper.Map<AuthenticationDto>(model);
            _authModule.InviteAdmin(dto, this._CurrentUserInfo.Id);
            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.AuthenticationController_InvitePlayer
            });
        }

        [Route("/InviteAdmin/{key}")]
        [Authorize]
        public IActionResult InviteAdmin(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true};
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
                    return View(model);
                }
                else
                {
                    throw ex;
                }
            }

            model.Message = WebApiTranslations.AuthenticationController_ValidKeyInviteAdmin;
            return View(model);
        }

        [Route("/LostPassword/{key}")]
        public IActionResult LostPassword(string key)
        {
            var model = new AuthenticationModel { IsKeyValid = true };
            try
            {
                model.AuthId = _authModule.ConfirmEmailReset(key);
            }
            catch (Exception ex)
            {
                if (ex is UserDataException || ex is Common.Exceptions.InvalidDataException)
                {
                    model.IsKeyValid = false;
                    model.Message = ex.Message;
                    return View(model);
                }
                else
                {
                    throw ex;
                }
            }

            return View(model);
        }
    }
}

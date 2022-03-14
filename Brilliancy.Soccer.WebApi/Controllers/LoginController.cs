using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Translations;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Translations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        public LoginController(IMapper mapper, ILoginModule loginModule, IApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, loginModule, httpContextAccessor)
        {
            _userManager = userManager;
        }

        public ActionResult Index(string returnUrl = null)
        {
            return View();
        }

        [Authorize]
        [Route("Test")]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginWriteModel credentials)
        {
            if (ModelState.IsValid)
            {
                var loginDto = _userManager.LoginUser(credentials.Login, credentials.Password);
                if (loginDto == null)
                {
                    return new JsonResult(new BaseResultReadModel
                    {
                        IsSuccess = false,
                        Message = WebApiTranslations.LoginController_InvalidLoginOrPassword
                    });
                }

                // sign-in
                var res = await AuthUser(loginDto.Id, loginDto.Login, loginDto.Email, WebApiTranslations.LoginController_LoginSuccessful);
                return res;
            }
            else
            {
                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = false,
                    Message = WebApiTranslations.LoginController_InvalidLoginOrPassword
                });
            }
        }
        [HttpPost]
        [Route("ChangeLanguage")]
        public JsonResult ChangeLanguage(LanguageWriteModel model)
        {
            if (string.IsNullOrEmpty(model?.Name))
            {
                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = true,
                    Message = WebApiTranslations.BaseController_UnexpectedError
                }); ;
            }
            var newCulture = CultureInfo.CreateSpecificCulture(model.Name);
            WebApiTranslations.Culture = newCulture;
            CoreTranslations.Culture = newCulture;
            CommonTranslations.Culture = newCulture;
            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = true,
                Message = WebApiTranslations.LoginController_LanguageChanged
            });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var res = await LogoutContext(WebApiTranslations.LoginController_LogoutSuccessful);
            return res;
        }

        private async Task<IActionResult> LogoutContext(string message)
        {
            await HttpContext.SignOutAsync();

            return new JsonResult(new Dictionary<string, object>
                {
                { "access_token", "" },
                { "id_token", "" },
                { "message", message },
                { "isSuccess", true }
                });
        }

        private async Task<IActionResult> AuthUser(int id, string login, string email, string message)
        {
            await HttpContext.SignInAsync(
                scheme: "MTCookieScheme",
                principal: _userManager.AuthorizeUser(login, email));

            return new JsonResult(new Dictionary<string, object>
                {
                { "access_token", _userManager.GetAccessToken(login) },
                { "id_token", _userManager.GetIdToken(id, login) },
                { "message", message },
                { "isSuccess", true }
                });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterWriteModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<RegisterUserDto>(model);
                dto.Password = _userManager.GeneratePassword(model.Password);
                _loginModule.RegisterUser(dto);
                var loginDto = _loginModule.GetUser(dto.Login);

                var res = await AuthUser(loginDto.Id, loginDto.Login, loginDto.Email, WebApiTranslations.LoginController_RegisterSuccessful);
                return res;
            }

            if (ModelState.Keys.Any())
            {
                foreach (var item in ModelState.Keys)
                {
                    var obj = ModelState[item];
                    if (obj.Errors.Any())
                    {
                        return new JsonResult(new BaseResultReadModel
                        {
                            IsSuccess = false,
                            Message = obj.Errors[0].ErrorMessage
                        });
                    }
                }
            }

            return new JsonResult(new BaseResultReadModel
            {
                IsSuccess = false,
                Message = WebApiTranslations.BaseController_UnexpectedError
            });
        }
    }
}

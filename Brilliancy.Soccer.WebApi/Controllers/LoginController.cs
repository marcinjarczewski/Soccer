using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Setup;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ILoginRepository _loginRepository;
        public LoginController(IMapper mapper, ILoginModule loginModule, ILoginRepository loginRepository, IApplicationUserManager userManager) : base(mapper, loginModule)
        {
            _userManager = userManager;
            _loginRepository = loginRepository;
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
        [Route("/api/login")]
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
                        Message = "Nieprawidłowy login lub hasło"
                    });
                }

                // sign-in
                var res = await AuthUser(loginDto.Id, loginDto.Login, loginDto.Email, "Logowanie zakończone powodzeniem");
                return res;
            }
            else
            {
                return new JsonResult(new BaseResultReadModel
                {
                    IsSuccess = false,
                    Message = "Nieprawidłowy login lub hasło"
                });
            }
        }

        [HttpPost]
        [Route("/api/logout")]
        public async Task<IActionResult> Logout()
        {
            var res = await LogoutContext("Zostałeś wylogowany");
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
        [Route("/api/register")]
        public async Task<IActionResult> Register(RegisterWriteModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<RegisterUserDto>(model);
                dto.Password = _userManager.GeneratePassword(model.Password);
                _loginModule.RegisterUser(dto);
                var loginDto = _loginModule.GetUser(dto.Login);

                var res = await AuthUser(loginDto.Id, loginDto.Login, loginDto.Email, "Rejestracja zakończona powodzeniem.");
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
                Message = "Wystąpił niespodziewany błąd."
            });
        }
    }
}

using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Models;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Brilliancy.Soccer.WebApi.Setup;
using Brilliancy.Soccer.WebApi.Translations;
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
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class TournamentController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly ITournamentModule _tournamentModule;
        public TournamentController(IMapper mapper, ILoginModule loginModule, ITournamentModule tournamentModule, IApplicationUserManager userManager) : base(mapper, loginModule)
        {
            _userManager = userManager;
            _tournamentModule = tournamentModule;
        }

        public ActionResult Index(string returnUrl = null)
        {
            var t = _tournamentModule.GetTournament(2,1);
            return View();
        } 
    }
}

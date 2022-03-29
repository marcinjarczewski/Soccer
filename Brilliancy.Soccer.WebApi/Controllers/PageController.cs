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
    public class PageController : BaseController
    {
        public PageController (IMapper mapper, ILoginModule loginModule, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, loginModule, httpContextAccessor)
        {
        }

        [Route("/Terms")]
        public ActionResult Terms()
        {
            return View();
        }
    }
}

using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class PageController : BaseController
    {
        public PageController (IMapper mapper, ILoginModule loginModule, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, loginModule, httpContextAccessor)
        {
        }

        [Route("/")]
        public ActionResult Home()
        {
            return View();
        }

        [Route("/Terms")]
        public ActionResult Terms()
        {
            return View();
        }

        [Route("/Policy")]
        public ActionResult Policy()
        {
            return View();
        }
    }
}

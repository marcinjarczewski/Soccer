using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    [Route("[controller]")]
    public class PageController : BaseController
    {
        private readonly ILogger<PageController> _logger;

        public PageController (IMapper mapper, ILoginModule loginModule, ILogger<PageController> logger, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, loginModule, httpContextAccessor)
        {
            _logger = logger;
        }

        [Route("/")]
        public ActionResult Home()
        {
            _logger.LogInformation($"Home visited");
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

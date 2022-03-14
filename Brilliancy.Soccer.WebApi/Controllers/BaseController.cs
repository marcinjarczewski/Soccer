using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper _mapper { get; }
        protected ILoginModule _loginModule { get; }

        private UserInfo _userInfo;
        public BaseController(IMapper mapper, ILoginModule loginModule, IHttpContextAccessor httpContextAccessor) : base()
        {
            _mapper = mapper;
            _loginModule = loginModule;
            var request = httpContextAccessor.HttpContext.Request;
            GlobalUrlHelper.AppUrl = $"{request.Scheme}://{request.Host}";
        }

        public UserInfo _CurrentUserInfo
        {
            get
            {
                if (_userInfo == null)
                {
                    var name = User?.Identity?.Name;
                    if (!string.IsNullOrEmpty(name))
                    {
                        var user = _loginModule.GetUser(name);
                        _userInfo = _mapper.Map<UserInfo>(user);
                    }
                }

                return _userInfo;
            }
        }

        //protected ActionResult JsonCamelCase(dynamic data)
        //{
        //    return new JsonResult(data);
        //}

        //public string SerializeCamelCase(dynamic data)
        //{
        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };
        //    return JsonConvert.SerializeObject(data, jsonSerializerSettings);
        //}
    }
}

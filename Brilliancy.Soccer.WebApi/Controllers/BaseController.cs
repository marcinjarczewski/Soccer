using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.WebApi.Models;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper _mapper { get; }
        protected ILoginModule _loginModule { get; }

        private UserInfo _userInfo;
        public BaseController(IMapper mapper, ILoginModule loginModule)
        {
            _mapper = mapper;
            _loginModule = loginModule;
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

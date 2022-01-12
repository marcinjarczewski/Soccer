using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.DbAccess;
using System;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class BaseModule 
    {
        protected IMapper _mapper {get;}
        public BaseModule(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}

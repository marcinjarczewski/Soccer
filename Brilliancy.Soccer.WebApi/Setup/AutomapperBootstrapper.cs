
using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Core;
using Brilliancy.Soccer.DbModels;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using System;
using System.Linq;

namespace Brilliancy.Soccer.WebApi.Setup
{
    public static class AutomapperBootstrapper
    {
        public static IMapper Init()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
                cfg.AddProfile<AutomapperWebProfile>();            
            });

            return configuration.CreateMapper();
        }
    }
}

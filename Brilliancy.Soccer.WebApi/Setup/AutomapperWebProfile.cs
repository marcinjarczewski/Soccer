
using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using System;
using System.Linq;

namespace Brilliancy.Soccer.WebApi.Setup
{
    public class AutomapperWebProfile : Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<RegisterWriteModel, RegisterUserDto>();

            CreateMap<LoginDto, UserInfo>()
               .ForMember(dto => dto.IsAdmin, m => m.MapFrom(db => (db.Roles ?? new System.Collections.Generic.List<RoleDto>()).Any(r => r.Id == (int)RoleEnum.Admin)));
        }
    }
}

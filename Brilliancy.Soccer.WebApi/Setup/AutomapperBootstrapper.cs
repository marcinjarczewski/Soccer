
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
    public static class AutomapperBootstrapper
    {
        public static IMapper Init()
        {
            var _configuraton = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
                cfg.CreateMap<bool?, bool>().ConvertUsing(new BoolDefaultTypeConverter());

                cfg.CreateMap<RegisterWriteModel, RegisterUserDto>();
                cfg.CreateMap<RegisterUserDto, UserDbModel>();

                cfg.CreateMap<UserDbModel, LoginDto>();
                cfg.CreateMap<RoleDbModel, RoleDto>();
                cfg.CreateMap<UserDbModel, UserDto>();

                cfg.CreateMap<LoginDto, UserInfo>()
                   .ForMember(dto => dto.IsAdmin, m => m.MapFrom(db => (db.Roles ?? new System.Collections.Generic.List<RoleDto>()).Any(r => r.Id == (int)RoleEnum.Admin)));
            });

            return _configuraton.CreateMapper();
        }
    }

    public class DateTimeTypeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString("dd.MM.yyyy");
        }
    }

    public class BoolDefaultTypeConverter : ITypeConverter<bool?, bool>
    {
        public bool Convert(bool? source, bool destination, ResolutionContext context)
        {
            return source ?? false;
        }
    }
}

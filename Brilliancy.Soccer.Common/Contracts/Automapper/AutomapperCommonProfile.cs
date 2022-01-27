using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Automapper
{
    public class AutomapperCommonProfile : Profile
    {
        public AutomapperCommonProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            CreateMap<bool?, bool>().ConvertUsing(new BoolDefaultTypeConverter());
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

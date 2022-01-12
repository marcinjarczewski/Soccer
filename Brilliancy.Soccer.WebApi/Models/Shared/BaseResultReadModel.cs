using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Shared
{
    public class BaseResultReadModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Shared
{
    public class BaseResultWithDataReadModel : BaseResultReadModel
    {
        public object Data { get; set; }
    }
}

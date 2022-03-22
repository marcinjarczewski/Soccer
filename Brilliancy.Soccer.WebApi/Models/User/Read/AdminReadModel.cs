using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.User.Read
{
    public class AdminReadModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PlayerId { get; set; }
    }
}

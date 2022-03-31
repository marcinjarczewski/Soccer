using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Read
{
    public class MatchLiveModel
    {
        public int Id { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}

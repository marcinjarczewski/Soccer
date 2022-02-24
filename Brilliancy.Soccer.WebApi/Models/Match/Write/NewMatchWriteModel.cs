using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Write
{
    public class NewMatchWriteModel
    {
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int TournamentId { get; set; }
    }
}

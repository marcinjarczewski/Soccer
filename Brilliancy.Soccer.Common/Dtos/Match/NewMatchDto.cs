using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class NewMatchDto
    {
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int TournamentId { get; set; }
    }
}

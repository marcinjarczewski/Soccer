using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class NewMatchDto
    {
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int TournamentId { get; set; }
    }
}

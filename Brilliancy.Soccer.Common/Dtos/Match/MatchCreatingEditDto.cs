using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class MatchCreatingEditDto
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime? Date { get; set; }

        public List<Player.PlayerDto> HomePlayers { get; set; }

        public List<Player.PlayerDto> AwayPlayers { get; set; }
    }
}

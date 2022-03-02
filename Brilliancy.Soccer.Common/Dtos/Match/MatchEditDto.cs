using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class MatchEditDto
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public int HalfHomeGoals { get; set; }

        public int HalfAwayGoals { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public List<Player.PlayerDto> HomePlayers { get; set; }

        public List<Player.PlayerDto> AwayPlayers { get; set; }

        public List<GoalDto> Goals { get; set; }

        public List<Player.PlayerDto> AvailablePlayers { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

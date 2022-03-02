using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Read
{
    public class MatchDetailsModel
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

        public List<Player.Read.PlayerReadModel> HomePlayers { get; set; }

        public List<Player.Read.PlayerReadModel> AwayPlayers { get; set; }

        public List<GoalReadModel> HomeGoalsList { get; set; }

        public List<GoalReadModel> AwayGoalsList { get; set; }

        public List<Player.Read.PlayerReadModel> AvailablePlayers { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public GoalReadModel EmptyGoal { get; set; }
    }
}

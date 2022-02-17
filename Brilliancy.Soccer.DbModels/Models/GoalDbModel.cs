using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class GoalDbModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public MatchDbModel Match { get; set; }

        public int MatchId { get; set; }

        public int? Time { get; set; }

        public PlayerDbModel Scorer { get; set; }

        public int ScorerId { get; set; }

        public PlayerDbModel Assist { get; set; }

        public int? AssistId { get; set; }

        public bool IsOwnGoal { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}

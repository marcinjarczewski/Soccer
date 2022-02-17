using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class GoalDto
    {
        public int? Id { get; set; }

        public int ScorerId { get; set; }

        public int? Time { get; set; }

        public int? AssistId { get; set; }

        public bool IsOwnGoal { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}

using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class MatchPendingEditDto
    {
        public int Id { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime? Date { get; set; }

        public List<GoalDto> Goals { get; set; }
    }
}

using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Match
{
    public class MatchOngoingEditDto
    {
        public int Id { get; set; }

        public GoalDto Goal { get; set; }
    }
}

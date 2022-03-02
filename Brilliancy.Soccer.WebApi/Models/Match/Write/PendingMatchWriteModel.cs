using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Write
{
    public class PendingMatchWriteModel
    {
        public int Id { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime? Date { get; set; }

        public List<GoalWriteModel> HomeGoalsList { get; set; }

        public List<GoalWriteModel> AwayGoalsList { get; set; }
    }
}

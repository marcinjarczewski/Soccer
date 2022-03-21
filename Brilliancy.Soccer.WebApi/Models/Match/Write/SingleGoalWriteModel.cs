using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Write
{
    public class SingleGoalWriteModel : GoalWriteModel
    {
        public bool IsHomeTeam { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Write
{
    public class MatchOngoingEditModel
    {
        public int Id { get; set; }

        public SingleGoalWriteModel Goal { get; set; }
    }
}

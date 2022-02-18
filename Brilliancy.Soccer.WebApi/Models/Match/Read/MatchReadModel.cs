using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Read
{
    public class MatchReadModel
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public int HalfHomeGoals { get; set; }

        public int HalfAwayGoals { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

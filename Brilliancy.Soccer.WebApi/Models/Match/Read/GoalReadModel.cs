using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Read
{
    public class GoalReadModel
    {
        public int Id { get; set; }

        public string ScorerPlayerName { get; set; }

        public string AssistPlayerName { get; set; }

        public int ScorerId { get; set; }

        public int? AssistId { get; set; }

        public bool IsOwnGoal { get; set; }

        public int Time { get; set; }
    }
}

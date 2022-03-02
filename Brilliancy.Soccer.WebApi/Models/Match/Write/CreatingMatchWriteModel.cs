using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Match.Write
{
    public class CreatingMatchWriteModel
    {
        public int Id { get; set; }
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime? Date { get; set; }

        public List<Player.Write.PlayerWriteModel> HomePlayers { get; set; }

        public List<Player.Write.PlayerWriteModel> AwayPlayers { get; set; }
    }
}

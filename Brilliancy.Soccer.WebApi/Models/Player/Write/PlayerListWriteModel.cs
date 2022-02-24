using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Player.Write
{
    public class PlayerListWriteModel
    {
        public List<PlayerWriteModel> Players { get; set; }

        public int TournamentId { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class TeamDbModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public TournamentDbModel Tournament { get; set; }

        public int TournamentId { get; set; }

        public List<PlayerDbModel> Players { get; set; }
    }
}

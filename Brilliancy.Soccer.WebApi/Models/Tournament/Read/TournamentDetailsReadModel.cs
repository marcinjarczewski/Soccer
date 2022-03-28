using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Player.Read;
using Brilliancy.Soccer.WebApi.Models.User.Read;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.WebApi.Models.Read.Tournament
{
    public class TournamentDetailsReadModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string LogoUrl { get; set; }

        public MatchReadModel NextMatch { get; set; }

        public MatchReadModel LastMatch { get; set; }

        public List<PlayerReadModel> Players { get; set; }

        public List<MatchReadModel> Matches { get; set; }
    }
}

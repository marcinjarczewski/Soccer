using Brilliancy.Soccer.WebApi.Models.Match.Read;
using System;

namespace Brilliancy.Soccer.WebApi.Models.Read.Tournament
{
    public class TournamentListReadModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string LogoUrl { get; set; }

        public MatchReadModel NextMatch { get; set; }

        public MatchReadModel LastMatch { get; set; }
    }
}

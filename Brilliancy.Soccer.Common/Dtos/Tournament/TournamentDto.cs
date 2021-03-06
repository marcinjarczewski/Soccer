using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Tournament
{
    public class TournamentDto
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public UserDto Owner { get; set; }

        public string Address { get; set; }

        public int? LogoId { get; set; }

        public string LogoUrl { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public MatchEditDto NextMatch { get; set; }

        public MatchEditDto LastMatch { get; set; }

        public TimeSpan? DefaultHour { get; set; }

        public List<AdminDto> Admins { get; set; }

        public List<PlayerDto> Players { get; set; }

        public List<MatchEditDto> Matches { get; set; }
    }
}

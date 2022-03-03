using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Tournament
{
    public class NewTournamentDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }

        public int? LogoId { get; set; }

        public string LogoUrl { get; set; }

        public string Address { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public TimeSpan? DefaultHour { get; set; }

        public List<int> Admins { get; set; }
    }
}

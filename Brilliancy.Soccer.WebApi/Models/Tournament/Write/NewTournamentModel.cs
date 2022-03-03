using System;

namespace Brilliancy.Soccer.WebApi.Models.Write.Tournament
{
    public class NewTournamentModel
    {
        public string Name { get; set; }

        public int? LogoId { get; set; }

        public string LogoUrl { get; set; }

        public string Address { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public TimeSpan? DefaultHour { get; set; }
    }
}

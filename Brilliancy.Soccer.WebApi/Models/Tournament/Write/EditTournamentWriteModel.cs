using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Player.Read;
using Brilliancy.Soccer.WebApi.Models.User.Read;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.WebApi.Models.Read.Tournament
{
    public class EditTournamentWriteModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int? LogoId { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public TimeSpan? DefaultHour { get; set; }
    }
}

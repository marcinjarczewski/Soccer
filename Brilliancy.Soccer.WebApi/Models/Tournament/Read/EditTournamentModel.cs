using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Player.Read;
using Brilliancy.Soccer.WebApi.Models.User.Read;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.WebApi.Models.Read.Tournament
{
    public class EditTournamentModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public bool IsOwner { get; set; }

        public string Address { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public TimeSpan? DefaultHour { get; set; }

        public MatchReadModel NextMatch { get; set; }

        public List<UserReadModel> Admins { get; set; }

        public List<PlayerReadModel> Players { get; set; }

        public List<MatchReadModel> Matches { get; set; }

        public PlayerReadModel EmptyPlayer { get; set; }

        public UserReadModel EmptyUser { get; set; }

        public NewMatchWriteModel EmptyMatch { get; set; }
    }
}

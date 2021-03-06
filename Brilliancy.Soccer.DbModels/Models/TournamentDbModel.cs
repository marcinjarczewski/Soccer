using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brilliancy.Soccer.DbModels
{
    public class TournamentDbModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public FileDbModel Logo { get; set; }

        public UserDbModel Owner { get; set; }

        public int OwnerId { get; set; }

        public string Address { get; set; }

        public int? DefaultDayOfTheWeek { get; set; }

        public TimeSpan? DefaultHour { get; set; }

        public List<TeamDbModel> Teams { get; set; }

        public List<UserDbModel> Admins { get; set; }

        public List<PlayerDbModel> Players { get; set; }

        public List<MatchDbModel> Matches { get; set; }
    }
}

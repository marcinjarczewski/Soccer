using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class MatchDbModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public TeamDbModel HomeTeam { get; set; }

        public int HomeTeamId { get; set; }

        public TeamDbModel AwayTeam { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public int HalfHomeGoals { get; set; }

        public int HalfAwayGoals { get; set; }

        public List<GoalDbModel> Goals { get; set; }

        public MatchStateDbModel State { get; set; }

        public int StateId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TournamentDbModel Tournament { get; set; }

        public int TournamentId { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class PlayerDbModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public TournamentDbModel Tournament { get; set; }

        public int TournamentId { get; set; }

        public UserDbModel User { get; set; }

        public int? UserId { get; set; }
    }
}

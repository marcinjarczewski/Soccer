using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class UserDbModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public List<UserRoleDbModel> UserRoles { get; set; }

        public List<PlayerDbModel> Players { get; set; }

        public List<AuthenticationDbModel> Authentications { get; set; }

        public List<TournamentDbModel> TournamentAdmins { get; set; }

        public List<TournamentDbModel> OwnedTournaments { get; set; }
    }
}

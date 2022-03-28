using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts
{
    public class UserInfo
    {
        public string Login { get; set; }

        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        public List<int> TournamentAdmins { get; set; }
    }
}

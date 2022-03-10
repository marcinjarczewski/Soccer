using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class AuthenticationTypeDbModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AuthenticationDbModel> Authentications { get; set; }
    }
}

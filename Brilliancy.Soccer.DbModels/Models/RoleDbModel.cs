using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class RoleDbModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserRoleDbModel> UserRoles { get; set; }
    }
}

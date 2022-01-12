namespace Brilliancy.Soccer.DbModels
{
   public class UserRoleDbModel
    {
        public UserDbModel User { get; set; }

        public int UserId { get; set; }

        public RoleDbModel Role { get; set; }

        public int RoleId { get; set; }
    }
}

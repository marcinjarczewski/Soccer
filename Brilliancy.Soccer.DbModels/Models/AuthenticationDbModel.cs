using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class AuthenticationDbModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public DateTime? DateValidaty { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Data { get; set; }

        public UserDbModel CreatedByUser { get; set; }

        public int CreatedByUserId { get; set; }

        public AuthenticationTypeDbModel Type { get; set; }

        public int TypeId { get; set; }
    }
}

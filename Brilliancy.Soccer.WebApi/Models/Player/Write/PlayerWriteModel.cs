using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Player.Write
{
    public class PlayerWriteModel
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }
    }
}

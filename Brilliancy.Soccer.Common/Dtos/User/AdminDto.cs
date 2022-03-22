using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Dtos.User
{
    public class AdminDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PlayerId { get; set; }
    }
}

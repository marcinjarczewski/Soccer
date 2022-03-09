using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Email
{
    public class EmailDto
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Recipient { get; set; }

        public string Address { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? DateSent { get; set; }

        public int Counter { get; set; }

        public string LastErrorMessage { get; set; }

        public DateTime? LastErrorDate { get; set; }
    }
}

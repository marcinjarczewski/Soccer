using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class EmailDbModel
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

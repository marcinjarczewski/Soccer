using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Brilliancy.Soccer.DbModels
{
    public class ConfigurationDbModel
    {
        public string Description { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}

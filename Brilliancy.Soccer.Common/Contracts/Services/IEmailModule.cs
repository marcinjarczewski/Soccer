using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Email is added as a part of a transaction. Call save chages manually after using it.
        /// </summary>
        /// <param name="emailAdrress"></param> 
        /// <param name="name"></param>
        /// <param name="appUrl"></param>
        /// <param name="language"></param>
        void AddWelcomeEmail(string emailAdrress, string name, string appUrl, LanguageEnum language);
    }
}

using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IEmailModule
    {
        void SentWelcomeEmail(string emailAdrress, string name, string appUrl, LanguageEnum language);
    }
}

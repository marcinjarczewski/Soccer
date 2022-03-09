using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services.EmailSender;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Core.Services.EmailSender
{
    public class SmtpClientFactory
    {
        public static ISmtpClient GetSmtpClient(IConfigurationRepository configurationRepository)
        {
            var emailAddress = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterAddress);
            var emailPassword = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPassword);
            var emailHost = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSMTP);
            int emailPort = int.Parse(configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPort));
            return new SmtpClientAdapter(emailHost, emailPort, emailAddress, emailPassword);
        }
    }
}

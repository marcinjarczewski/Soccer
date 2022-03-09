using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services.EmailSender;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Services.EmailSender
{
    internal class EmailSenderLogic : IEmailSenderLogic
    {
        private IEmailRepository _emailRepository { get; }
        private IConfigurationRepository _configurationRepository { get; }
        private ISmtpClient _smtpClient { get; }
        private IEmailCreator _emailCreator { get; }

        public EmailSenderLogic(IEmailRepository emailRepository, IConfigurationRepository configurationRepository, ISmtpClient smtpClient, IEmailCreator emailCreator)
        {
            _emailRepository = emailRepository;
            _configurationRepository = configurationRepository;
            _smtpClient = smtpClient;
            _emailCreator = emailCreator;
        }

        internal virtual bool IsSendingTime(List<int> timeTable, int emailCounter, DateTime addedDate)
        {
            if(emailCounter == 0)
            {
                return true;
            }

            if(timeTable == null)
            {
                return false;
            }

            if(emailCounter > timeTable.Count)
            {
                return false;
            }

            var minutes = timeTable.ElementAt(emailCounter - 1);
            return addedDate.AddMinutes(minutes) < DateTime.Now;
        }

        internal virtual List<int> GetTimeTable()
        {
            var emailSendingTime = _configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSendingTime);
            return emailSendingTime.Split(';').Select(t => int.Parse(t)).ToList();
        }

        internal List<EmailDto> GetEmails()
        {       
            return _emailRepository.GetEmailsToSend(GetTimeTable().Count());
        }

        public int Send(List<EmailDto> emails)
        {
            try
            {
                int emailServiceSleepTime = int.Parse(_configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSleepTime));
                var timeTable = GetTimeTable();
                if (emails.Count > 0)
                {
                    var emailAddress = _configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterAddress);
                    var emailName = _configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterName);
                    bool emailSSL = bool.Parse(_configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSSLEnabled));
                    var emailRegisterReplyTo = _configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterReplyTo);

                    _smtpClient.EnableSsl = emailSSL;
                    foreach (var email in emails)
                    {
                        if (IsSendingTime(timeTable, email.Counter, email.AddedDate))
                        {
                            try
                            {
                                var mailMessage = _emailCreator.CreateMessage(email, emailAddress, emailName, emailRegisterReplyTo);
                                _smtpClient.Send(mailMessage);
                                email.DateSent = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                email.Counter++;
                                email.LastErrorMessage = ex.Message;
                                email.LastErrorDate = DateTime.Now;
                            }

                            _emailRepository.Update(email);
                        }
                    }
                }
                return emailServiceSleepTime;
            }
            catch
            {
               // ignore error and try again later
               // throw new UserDataException(Core.Translations.CoreTranslations.EmailSender_WrongConfig);
            }

            return 60 * 1000; // one minute default
        }
    }
}

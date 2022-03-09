using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace Brilliancy.Soccer.Core.Services
{
    public class EmailSenderService
    {
        private volatile bool isRunning = false;
        protected AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private static readonly object _lock = new object();

        private IConfigurationRepository _configurationRepository;
        private IEmailRepository _emailRepository;
        private EmailSenderService() { }
        private static EmailSenderService _instance;

        public void WakeUp()
        {
            _instance.WaitHandle.Set();
        }

        public EmailSenderService(IConfigurationRepository configurationRepository,IEmailRepository emailRepository)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new EmailSenderService();
                        _instance._configurationRepository = configurationRepository;
                        _instance._emailRepository = emailRepository;
                    }
                }
            }
        }

        public static EmailSenderService GetInstance()
        {
            return _instance;
        }

        internal void Start()
        {
            var thread = new Thread(new ThreadStart(this.Running));
            thread.IsBackground = false;

            this.isRunning = true;
            thread.Start();
        }

        internal void Stop()
        {
            this.isRunning = false;
        }

        private void Running()
        {
            while (this.isRunning)
            {
                this.Sleep(this.Send());
            }
        }
        private bool IsSendingTime(IEnumerable<int> timeTable, EmailDto email)
        {
            return true;
        }

        private int Send()
        {
            try
            {
                var configurationRepository = _instance._configurationRepository;
                var emailRepository = _instance._emailRepository;

                var emailSendingTime = configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSendingTime);
                var timeTable = emailSendingTime.Split(';').Select(t => int.Parse(t));
                var emails = emailRepository.GetEmailsToSend(timeTable.Count());
                int emailServiceSleepTime = int.Parse(configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSleepTime));
                if (emails.Count > 0)
                {
                    var emailAddress = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterAddress);
                    var emailName = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterName);
                    var emailPassword = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPassword);
                    var emailHost = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSMTP);
                    int emailPort = int.Parse(configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPort));
                    bool emailSSL = bool.Parse(configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSSLEnabled));
                    var emailRegisterReplyTo = configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterReplyTo);

                    var smtp = new SmtpClient(emailHost, emailPort);
                    if (emailSSL) smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(emailAddress, emailPassword);

                    foreach (var email in emails)
                    {
                        if (IsSendingTime(timeTable, email))
                        {
                            try
                            {
                                MailMessage mailMessage = new MailMessage { IsBodyHtml = true };
                                mailMessage.From = new MailAddress(emailAddress, emailName);
                                mailMessage.ReplyToList.Add(new MailAddress(emailRegisterReplyTo));
                                mailMessage.Subject = email.Subject;

                                if (string.IsNullOrEmpty(email.Address))
                                {
                                    throw new NullReferenceException("Recipient is null");
                                }

                                if (string.IsNullOrEmpty(email.Recipient))
                                {
                                    mailMessage.To.Add(new MailAddress(email.Address));
                                }
                                else
                                {
                                    mailMessage.To.Add(new MailAddress(email.Address, email.Recipient));
                                }

                                var htmlView = AlternateView.CreateAlternateViewFromString(email.Body, null, MediaTypeNames.Text.Html);
                                mailMessage.AlternateViews.Add(htmlView);

                                smtp.Send(mailMessage);
                                email.DateSent = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                email.Counter++;
                                email.LastErrorMessage = ex.Message;
                            }

                            emailRepository.Update(email);
                        }
                    }
                }
                return emailServiceSleepTime;
            }
            catch (Exception ex)
            {

            }

            return 60 * 1000; // one minute default
        }
        private void Sleep(int time)
        {
            _instance.WaitHandle.WaitOne(time);
        }
    }
}

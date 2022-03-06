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

        protected AutoResetEvent WaitHandle =  new AutoResetEvent(false);

        private static readonly object _lock = new object();
        private IConfigurationRepository configurationRepository;
        private IEmailRepository emailRepository;

        private EmailSenderService() { }
        private static EmailSenderService _instance;

        public void WakeUp()
        {
            _instance.WaitHandle.Set();
        }

        public static EmailSenderService Init(
            IConfigurationRepository configurationRepository,
            IEmailRepository emailRepository)
        {

            _instance = new EmailSenderService();
            _instance.configurationRepository = configurationRepository;
            _instance.emailRepository = emailRepository;

            return _instance;
        }

        public static EmailSenderService GetInstance()
        {
            if(_instance == null)
            {
                throw new NullReferenceException("");
            }

            return _instance;
        }

        public void Start()
        {
            var thread = new Thread(new ThreadStart(this.Running));
            thread.IsBackground = false;

            this.isRunning = true;
            thread.Start();
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        public void Running()
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
            var emails = this.emailRepository.GetEmailsToSend();
            int emailServiceSleepTime = int.Parse(this.configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSleepTime));

            if (emails.Count > 0)
            {
                var emailAddress = this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterAddress);
                var emailName = this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterName);
                var emailPassword = this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPassword);
                var emailHost = this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSMTP);
                int emailPort = int.Parse(this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterPort));
                bool emailSSL = bool.Parse(this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterSSLEnabled));
                var emailRegisterReplyTo = this.configurationRepository.GetValue(ConfigurationDictionary.EmailRegisterReplyTo);
                var emailSendingTime = this.configurationRepository.GetValue(ConfigurationDictionary.EmailServiceSendingTime);
                var timeTable = emailSendingTime.Split(';').Select(t => int.Parse(t));

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

        private void Sleep(int time)
        {
            _instance.WaitHandle.WaitOne(time);
        }
    }
}

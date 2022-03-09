using Brilliancy.Soccer.Common.Contracts.Services.EmailSender;
using System.Net;
using System.Net.Mail;

namespace Brilliancy.Soccer.Core.Services.EmailSender
{
    internal class SmtpClientAdapter : ISmtpClient
    {
        private SmtpClient _smtpClient { get; set; }

        public bool EnableSsl
        {
            get => _smtpClient.EnableSsl;
            set => _smtpClient.EnableSsl = value;
        }

        public SmtpClientAdapter(string host, int port, string address, string password)
        {
            _smtpClient = new SmtpClient(host, port);
            _smtpClient.Credentials = new NetworkCredential(address, password);
        }

        public void Send(MailMessage mailMessage)
        {
            _smtpClient.Send(mailMessage);
        }
    }
}

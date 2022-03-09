using System.Net;
using System.Net.Mail;

namespace Brilliancy.Soccer.Common.Contracts.Services.EmailSender
{
    public interface ISmtpClient
    {
        public bool EnableSsl { get; set; }

        void Send(MailMessage mailMessage);
    }
}

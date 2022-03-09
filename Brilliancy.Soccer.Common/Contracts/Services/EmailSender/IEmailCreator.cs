using Brilliancy.Soccer.Common.Dtos.Email;
using System.Net.Mail;

namespace Brilliancy.Soccer.Common.Contracts.Services.EmailSender
{
    public interface IEmailCreator
    {
        MailMessage CreateMessage(EmailDto email, string emailAddress, string emailName, string emailRegisterReplyTo);
    }
}

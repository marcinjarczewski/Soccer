using Brilliancy.Soccer.Common.Contracts.Services.EmailSender;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Core.Translations;
using System;
using System.Net.Mail;
using System.Net.Mime;

namespace Brilliancy.Soccer.Core.Services.EmailSender
{
    public class EmailCreator : IEmailCreator
    {
       public MailMessage CreateMessage(EmailDto email, string emailAddress, string emailName, string emailRegisterReplyTo)
        {
            if (email == null)
            {
                throw new NullReferenceException(CoreTranslations.EmailSender_NoEmail);
            }
            var mailMessage = new MailMessage { IsBodyHtml = true };
            mailMessage.From = new MailAddress(emailAddress, emailName);
            if (!string.IsNullOrEmpty(emailRegisterReplyTo))
            {
                mailMessage.ReplyToList.Add(new MailAddress(emailRegisterReplyTo));
            }
            mailMessage.Subject = email.Subject;

            if (string.IsNullOrEmpty(email.Address))
            {
                throw new NullReferenceException(CoreTranslations.EmailSender_NoEmailAddress);
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
            return mailMessage;
        }
    }
}

using Brilliancy.Soccer.Common.Dtos.Email;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts.Repositories
{
    public interface IEmailRepository
    {
        IList<EmailDto> GetEmailsToSend(int maxCounter);

        EmailDto Update(EmailDto dto);
    }
}

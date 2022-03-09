using Brilliancy.Soccer.Common.Dtos.Email;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts.Repositories
{
    public interface IEmailRepository
    {
        List<EmailDto> GetEmailsToSend(int maxFailCounter);

        void Update(EmailDto dto);
    }
}

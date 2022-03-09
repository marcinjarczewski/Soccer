using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Services.EmailSender
{
    public interface IEmailSenderLogic
    {
        int Send(List<EmailDto> emails);
    }
}

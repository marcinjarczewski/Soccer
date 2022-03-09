using Brilliancy.Soccer.Common.Contracts.Providers;
using Brilliancy.Soccer.Common.Dtos.Configuration;

namespace Brilliancy.Soccer.Common.Providers
{
    public class FtpClientFactory : IFtpClientFactory
    {
        public IFtpClient CreateFtpClient(ConfigurationDto dto)
        {
            return new FtpClient(dto);
        }
    }
}
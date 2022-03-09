using Brilliancy.Soccer.Common.Dtos.Configuration;

namespace Brilliancy.Soccer.Common.Contracts.Providers
{
    public interface IFtpClientFactory
    {
        IFtpClient CreateFtpClient(ConfigurationDto dto);
    }
}

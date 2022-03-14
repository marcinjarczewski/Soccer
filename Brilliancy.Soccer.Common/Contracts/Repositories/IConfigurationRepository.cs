namespace Brilliancy.Soccer.Common.Contracts.Repositories
{
    public interface IConfigurationRepository
    {
        string GetValue(string key);
    }
}

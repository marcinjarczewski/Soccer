using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.DbAccess;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class ConfigurationModule : IConfigurationRepository
    {
        private SoccerDbContext _dbContext { get; }
        public ConfigurationModule(SoccerDbContext context)
        {
            _dbContext = context;
        }

        public string GetValue(string key)
        {
            return _dbContext.Configurations.FirstOrDefault(f => f.Key == key)?.Value ?? string.Empty;
        }
    }
}

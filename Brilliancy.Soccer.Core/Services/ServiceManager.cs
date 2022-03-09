using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.Core.Services
{
    public class ServiceManager : IHostedService, IDisposable
    {
        protected AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private static readonly object _lock = new object();

        private EmailSenderService _emailSenderService;
        private IServiceScope _scope;
        private ServiceManager() { }
        private static ServiceManager _instance;

        public ServiceManager(IServiceProvider serviceProvider)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = new ServiceManager();
                    _instance._scope = serviceProvider.CreateScope();
                    _instance._emailSenderService = new EmailSenderService(GetConfigurationRepository(), GetEmailRepository());
                }
            }
        }

        private IEmailRepository GetEmailRepository()
        {
            return _instance._scope.ServiceProvider.GetService<IEmailRepository>();
        }

        private IConfigurationRepository GetConfigurationRepository()
        {
            return _instance._scope.ServiceProvider.GetService<IConfigurationRepository>();
        }

        public static ServiceManager GetInstance()
        {
            return _instance;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _instance._emailSenderService.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _instance._emailSenderService.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _scope?.Dispose();
            _instance?._scope?.Dispose();
            _instance?.Dispose();
        }
    }
}

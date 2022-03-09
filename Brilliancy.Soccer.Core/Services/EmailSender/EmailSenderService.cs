using Brilliancy.Soccer.Common.Contracts.Repositories;
using System.Threading;

namespace Brilliancy.Soccer.Core.Services.EmailSender
{
    public class EmailSenderService
    {
        private volatile bool isRunning = false;
        protected AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private static readonly object _lock = new object();

        private IConfigurationRepository _configurationRepository;
        private IEmailRepository _emailRepository;
        private EmailSenderService() { }
        private static EmailSenderService _instance;

        public void WakeUp()
        {
            _instance.WaitHandle.Set();
        }

        public EmailSenderService(IConfigurationRepository configurationRepository,IEmailRepository emailRepository)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new EmailSenderService();
                        _instance._configurationRepository = configurationRepository;
                        _instance._emailRepository = emailRepository;
                    }
                }
            }
        }

        public static EmailSenderService GetInstance()
        {
            return _instance;
        }

        internal void Start()
        {
            var thread = new Thread(new ThreadStart(this.Running));
            thread.IsBackground = false;

            this.isRunning = true;
            thread.Start();
        }

        internal void Stop()
        {
            this.isRunning = false;
        }

        private void Running()
        {
            while (this.isRunning)
            {
                this.Sleep(this.Send());
            }
        }

        internal int Send()
        {
            var logic = new EmailSenderLogic(
                _instance._emailRepository,
                _instance._configurationRepository,
                SmtpClientFactory.GetSmtpClient(_instance._configurationRepository), 
                new EmailCreator());
            return logic.Send(logic.GetEmails());
        }
        private void Sleep(int time)
        {
            _instance.WaitHandle.WaitOne(time);
        }
    }
}

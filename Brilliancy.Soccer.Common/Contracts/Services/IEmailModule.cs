using Brilliancy.Soccer.Common.Enums;

namespace Brilliancy.Soccer.Common.Contracts.Services
{
    public interface IEmailService
    {
        void AddWelcomeEmail(string emailAdrress, string name, string appUrl, LanguageEnum language);

        void AddPlayerInviteEmail(string emailAdrress, string name, string tournamentName, string key, string appUrl, LanguageEnum language);

        void AddForgottenPasswordEmail(string emailAdrress, string name, string key, string appUrl, LanguageEnum language);
    }
}

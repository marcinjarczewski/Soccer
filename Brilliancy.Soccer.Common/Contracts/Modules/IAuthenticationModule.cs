using Brilliancy.Soccer.Common.Dtos.Authentication;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IAuthenticationModule
    {
        void InvitePlayer(AuthenticationDto dto, int userId);

        void InviteAdmin(AuthenticationDto dto, int userId);

        void ConfirmPlayerInvitation(string key, int userId);

        void ConfirmAdminInvitation(string key, int userId);

        int ConfirmEmailReset(string key);

        void ForgottenPassword(string email);

        int GetUserFromAuth(int authId);
    }
}

using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Core.Modules.Authentication
{
    class ConfirmPlayerInvate :ConfirmLinkTemplate
    {
        public ConfirmPlayerInvate(SoccerDbContext context) : base(context, Common.Enums.AuthenticationTypeEnum.TournamentPlayerInvite)
        {
        }

        public override void ProccessData(Dictionary<string, int> dataDictionary, int userId)
        {
            var player = GetPlayer(dataDictionary, userId);
            if (player.UserId.HasValue)
            {
                throw new UserDataException(CoreTranslations.Authentication_PlayerAlreadyHasUser);
            }
            player.UserId = userId;
        }
    }
}

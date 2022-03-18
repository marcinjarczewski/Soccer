using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules.Authentication
{
    class ConfirmAdminInvite : ConfirmLinkTemplate
    {
        public ConfirmAdminInvite(SoccerDbContext context) : base(context, Common.Enums.AuthenticationTypeEnum.TournamentAdminInvite)
        {
        }

        public override void ProccessData(Dictionary<string, int> dataDictionary, int userId)
        {
            var player = GetPlayer(dataDictionary, userId);
            if(!player.Tournament.Admins.Any(a => a.Id == userId))
            {
                if(player.User == null)
                {
                    throw new UserDataException(CoreTranslations.Authentication_NoUser);
                }
                player.Tournament.Admins.Add(player.User);
            }
        }
    }
}

using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules.Authentication
{
    class ConfirmResetPassword : ConfirmLinkTemplate
    {
        public int UserId { get; set; }
        public ConfirmResetPassword(SoccerDbContext context) : base(context, Common.Enums.AuthenticationTypeEnum.ResetPassword)
        {
        }

        public override void ProccessData(Dictionary<string, int> dataDictionary, int userId)
        { }

        public override void ProccessLink(string key, int? userId = default(int?))
        {
            var auth = Validate(key);
            Update(auth);
            var data = GetData(auth);
            var splitted = SplitData(data);
            if (!splitted.ContainsKey(EmailDataDictionary.UserId))
            {
                throw new Common.Exceptions.InvalidDataException(CoreTranslations.Authentication_InvalidAuthData);
            }
            this.UserId = splitted[EmailDataDictionary.UserId];
            Save();
        }
    }
}

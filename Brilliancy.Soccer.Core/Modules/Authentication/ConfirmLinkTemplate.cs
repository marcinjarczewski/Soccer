using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliancy.Soccer.Core.Modules.Authentication
{
     abstract class ConfirmLinkTemplate
    {
        protected SoccerDbContext _dbContext { get; }
        private AuthenticationTypeEnum _typeEnum { get; }
        public ConfirmLinkTemplate(SoccerDbContext context, AuthenticationTypeEnum typeEnum)
        {
            _dbContext = context;
            _typeEnum = typeEnum;
        }

        public AuthenticationDbModel Validate(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new UserDataException(CoreTranslations.Authentication_NoKey);
            }
            var auth = _dbContext.Authentications.FirstOrDefault(a => a.Key == key && a.TypeId == (int)_typeEnum);
            if (auth == null)
            {
                throw new UserDataException(CoreTranslations.Authentication_InvalidKey);
            }
            if (auth.ConfirmDate != null)
            {
                throw new UserDataException(CoreTranslations.Authentication_AlreadyConfirmed);
            }
            if (auth.DateValidaty < DateTime.Now)
            {
                throw new UserDataException(CoreTranslations.Authentication_KeyExpired);
            }
            return auth;
        }

        public virtual void Update(AuthenticationDbModel auth)
        {
            auth.ConfirmDate = DateTime.Now;
        }

        protected Dictionary<string,int> SplitData(string data)
        {
           return data.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(s => new KeyValuePair<string, int>(
                 s.Trim().Split(":")[0], int.Parse(s.Trim().Split(":")[1]))).ToDictionary(x => x.Key, y => y.Value);
        }

        public virtual string GetData(AuthenticationDbModel auth)
        {
            return auth.Data;
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }

        public PlayerDbModel GetPlayer(Dictionary<string, int> dataDictionary, int userId)
        {
            if (!_dbContext.Users.Any(u => u.Id == userId))
            {
                throw new InvalidDataException(CoreTranslations.Authentication_NoUser);
            }
            if (!dataDictionary.ContainsKey(EmailDataDictionary.PlayerId))
            {
                throw new InvalidDataException(CoreTranslations.Authentication_InvalidAuthData);
            }
            var player = _dbContext.Players.Include(p => p.User).Include(p => p.Tournament.Admins).FirstOrDefault(p => p.Id == dataDictionary[EmailDataDictionary.PlayerId]);
            if (player == null)
            {
                throw new InvalidDataException(CoreTranslations.Authentication_InvalidAuthData);
            }
            return player;
        }

        public abstract void ProccessData(Dictionary<string, int> dataDictionary, int userId);

        public virtual void ProccessLink(string key, int? userId = default(int?))
        {
            var auth = Validate(key);
            Update(auth);
            var data = GetData(auth);
            var splitted = SplitData(data);
            if (userId.HasValue)
            {
                ProccessData(splitted, userId.Value);
            }
            Save();
        }
    }
}


using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.WebApi.Setup;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Providers
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JWTSettings _options;
        public ApplicationUserManager(ILoginRepository loginRepository, IOptions<JWTSettings> options)
        {
            _loginRepository = loginRepository;
            _options = options.Value;
        }

        public LoginDto LoginUser(string login, string password)
        {
            var user =  _loginRepository.GetUser(login);
            if(user == null)
            {
                return null;
            }

            if (!CheckPassword(password, user.Password))
            {
                throw new UserDataException("Nieprawidłowy login lub hasło");
            }

            if (!user.IsActive)
            {
                throw new UserDataException("Dostęp do konta został zablokowany. Skontaktuj się z administratorem.");
            }

            return user;
        }

        public string GeneratePassword(string candidate)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var salt = _options.SecretKey;
            var sha1data = sha1.ComputeHash(GenerateStreamFromString(candidate + salt));
            return Convert.ToBase64String(sha1data);
        }

        public ClaimsPrincipal AuthorizeUser(string name, string email)
        {
            List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,  name),
                        new Claim(ClaimTypes.Email, email)
                    };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            return new ClaimsPrincipal(identity);
        }

        //public IList<string> GetRoles(string key)
        //{
        //    return null;
        //}

     
        public bool IsInRole(LoginDto user, string roleName)
        {
            return user.Roles?.Any(r => r.Name == roleName) ?? false;
        }

        public string GetIdToken(int id, string login)
        {
            var payload = new Dictionary<string, object>
              {
                { "id", id },
                { "sub", login },
                //{ "email", user.Email },
                //{ "emailConfirmed", user.EmailConfirmed },
              };
            return GetToken(payload);
        }

        public string GetAccessToken(string username)
        {
            var payload = new Dictionary<string, object>
              {
                { "sub", username }
                //,{ "email", Email }
              };
            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aud", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private bool CheckPassword(string candidate, string hashedPassword)
        {
            var candidateHashed = GeneratePassword(candidate);
            return candidateHashed == hashedPassword;
        }

        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}

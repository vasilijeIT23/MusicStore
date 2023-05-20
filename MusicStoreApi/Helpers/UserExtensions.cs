using Microsoft.IdentityModel.Tokens;
using MusicStoreCore.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MusicStoreApi.Helpers
{
    public class UserExtensions
    {
        public static bool VerifyPassword(string password, string pwdHash, string pwdSalt)
        {
            if (password != null && pwdHash != null && pwdSalt != null)
            {
                var saltBytes = Convert.FromBase64String(pwdSalt);
                var checkPassword = Hash(password)+Hash(saltBytes.ToString());
                if (checkPassword == pwdHash)
                    return true;
            }
            return false;
        }

        public static string Hash(string stringToHash)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(stringToHash)));
        }

    }
}

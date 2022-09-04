using CardStorage.Services.AuthService;
using System.Security.Cryptography;
using System.Text;

namespace CardStorage.Utils
{
    public class Password
    {
        private string _passwordHash;
        private string _passwordSalt;
        private readonly string _secretKey = AuthService.SecretKey;

        public Password(string password, string salt = "")
        {
            byte[] buffer = RandomNumberGenerator.GetBytes(16);

            _passwordSalt = string.IsNullOrEmpty(salt) ? Convert.ToBase64String(buffer) : salt;

            buffer = Encoding.UTF8.GetBytes($"{password}{_passwordSalt}{_secretKey}");
            var sha512 = SHA512.Create();

            _passwordHash =  Convert.ToBase64String(sha512.ComputeHash(buffer));
        }

        public string Hash { get { return _passwordHash; } }
        public string Salt { get { return _passwordSalt; } }

        public static bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            return (new Password(password, passwordSalt)).Hash == passwordHash;
        }
    }
}

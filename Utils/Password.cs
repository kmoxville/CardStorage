using System.Security.Cryptography;
using System.Text;

namespace CardStorage.Utils
{
    public class Password
    {
        private string _passwordHash;
        private string _passwordSalt;
        private readonly string _secretKey = "test";

        public Password(string password)
        {
            byte[] buffer = RandomNumberGenerator.GetBytes(16);

            _passwordSalt = Convert.ToBase64String(buffer);

            buffer = Encoding.UTF8.GetBytes($"{password}{_passwordSalt}{_secretKey}");
            var sha512 = SHA512.Create();

            _passwordHash =  Convert.ToBase64String(sha512.ComputeHash(buffer));
        }

        public string Hash { get { return _passwordHash; } }

        public static bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            return (new Password(password)).Hash == passwordHash;
        }
    }
}

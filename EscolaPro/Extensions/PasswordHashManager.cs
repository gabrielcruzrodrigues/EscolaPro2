using System.Security.Cryptography;
using System.Text;

namespace EscolaPro.Extensions
{
    public static class PasswordHashManager
    {
        public static (string Hash, string Salt) HashGenerate(string password)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return (Convert.ToHexString(hash), Convert.ToHexString(salt));
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            byte[] saltBytes = Convert.FromHexString(storedSalt);

            byte[] enteredPasswordHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(enteredPassword),
                saltBytes,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(enteredPasswordHash) == storedHash;
        }
    }
}

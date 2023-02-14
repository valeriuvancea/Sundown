using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace MissionReportingTool.Helpers
{
    /// <summary>
    /// Inspired from https://medium.com/dealeron-dev/storing-passwords-in-net-core-3de29a3da4d2
    /// </summary>
    public class PasswordHelper
    {
        private const int Iterations = 1000; 
        private const int SaltSize = 16;
        private const int KeySize = 32;

        private PasswordHelper() { }

        public static string Hash(string password)
        {
            var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Iterations,
              HashAlgorithmName.SHA256);
            
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }

        public static bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KeySize);
            return keyToCheck.SequenceEqual(key);
        }
    }
}

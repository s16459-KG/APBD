using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Cw4.Pomocnicze
{
    public class PasswordHelper
    {

    public static string CreateSecretValue (string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            //Console.WriteLine("Wytworzono Hasz...");
            return Convert.ToBase64String(valueBytes);
        }

    public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using(var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

    public static bool Validate(string value, string salt, string hash)
        => CreateSecretValue(value, salt) == hash;

    }
}

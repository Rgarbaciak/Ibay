using System.Text;
using System.Security.Cryptography;

namespace Ibay.Tools
{
    public class HashPassword
    {
        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var byteArray = Encoding.Default.GetBytes(password);
            var hash = sha.ComputeHash(byteArray);
            return Convert.ToBase64String(hash);
        }
    }
}

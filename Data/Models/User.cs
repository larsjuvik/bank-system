using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public required string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        
        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdmin { get; set; } = false;

        public ICollection<BankAccount>? BankAccounts { get; set; }
        public ICollection<UserLogin>? UserLogins { get; set; }

        public static void CreateSaltAndHash(string password, out byte[] salt, out byte[] passwordHash)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }
            return true;
        }
    }
}
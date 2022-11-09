using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PorscheMarket.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        //Password hashing
        public static string HashPassword(string password)
        {
            using (var sha256=SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashPassword;
            }
        }
    }
}

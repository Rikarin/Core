using System;
using System.Security.Cryptography;
using System.Text;

namespace Rikarin.Helpers {
    public static class StringHelper {
        public static string GenerateRandomString(int length) {
            const string chrs = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var ret  = new char[length];
            var rand = new Random();

            for (var i = 0; i < length; i++) {
                ret[i] = chrs[rand.Next(0, chrs.Length)];
            }

            return new string(ret);
        }

        public static string HashPassword(string password) {
            var md5 = new MD5CryptoServiceProvider();
            var data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            return Convert.ToBase64String(data);
        }

        public static int? TryInt(this string str) {
            return int.TryParse(str, out int result) ? (int?)result : null;
        }
    }
}

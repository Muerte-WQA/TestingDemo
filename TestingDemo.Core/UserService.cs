using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TestingDemo.Core
{
    public class UserService
    {
        private const string Salt = "demo_salt_";
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _sessions = new Dictionary<string, string>();

        public bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Логин и пароль обязательны");
            if (password.Length < 8)
                throw new ArgumentException("Пароль должен быть не менее 8 символов");
            if (_users.ContainsKey(username))
                return false;

            _users[username] = HashPassword(password);
            return true;
        }

        public string Login(string username, string password)
        {
            if (!_users.ContainsKey(username))
                throw new ArgumentException("Пользователь не найден");
            if (_users[username] != HashPassword(password))
                throw new ArgumentException("Неверный пароль");

            var token = ToHexString(
                SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(username + Guid.NewGuid())));
            _sessions[token] = username;
            return token;
        }

        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(Salt + password);
                var hash = sha.ComputeHash(bytes);
                return ToHexString(hash);
            }
        }

        private static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
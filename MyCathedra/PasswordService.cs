using System;
using System.Security.Cryptography;
using System.Text;

namespace MyCathedra
{
    public class PasswordService
    {
        public string MakeHash(Guid userId, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым!", nameof(password));

            var bytes = Encoding.UTF8.GetBytes(userId + password);
            var hash = BitConverter.ToString(MD5.Create().ComputeHash(bytes));

            return hash;
        }
    }
}
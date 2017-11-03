using System;
using System.Data;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public static class CurrentUser
    {
        public static string Name;
    }

    public class User: BaseModel
    {
        private int _id;

        public int Timestamp { get; }
        public string Login { get; }
        public string PasswordHash { get; }
        public string Name { get; set; }
        public int Role { get; set; }

        public User(string login, string password, string name, int role)
        {
            string loginCandidate = login.Trim();
            if (SelectAll("SELECT count(id) FROM Users WHERE login = '" + loginCandidate + "'").Count > 0)
                throw new ArgumentException("Login must be unique", nameof(login));

            Login = loginCandidate;
            Timestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            PasswordHash = CryptoHelper.GetPasswordHash(password.Trim(), Login, Timestamp);
            Name = name.Trim();
            Role = role;
        }

        public User(string login)
        {
            string loginCandidate = login.Trim();

            var currentUser = SelectFirst("SELECT id, password, timestamp, name, role FROM Users WHERE login = '" + loginCandidate + "'");
            //if (currentUser == null || currentUser.Count == 0)
            if (currentUser.Count == 0)
                throw new ArgumentException("Can't find user with required login", nameof(login));

            Login = loginCandidate;
            _id = (int)currentUser[0];
            PasswordHash = (string)currentUser[1];
            Timestamp = (int)currentUser[2];
            Name = (string)currentUser[3];
            Role = (int)currentUser[4];
        }

        public bool CheckPassword(string password)
        {
            string hashOfInput = CryptoHelper.GetPasswordHash(password.Trim(), Login, Timestamp);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, PasswordHash)) return true;
            if (0 == comparer.Compare(password, PasswordHash)) return true;

            return false;
        }
    }
}

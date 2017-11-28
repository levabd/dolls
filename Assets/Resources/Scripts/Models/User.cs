using System;
using System.Collections.Generic;
using System.Data;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public static class CurrentUser
    {
        public static User User = User.FindById(1);
    }

    public class User: BaseModel
    {
        public enum UserRoles
        {
            Admin = 0,
            Manager = 1,
            User = 2
        }

        public int? Id { get; }
        public int Timestamp { get; private set; }
        public string Login { get; }
        public string PasswordHash { get; private set; }
        public string Name { get; set; }
        public UserRoles Role { get; set; }

        private void GeneratePwdAndTime(string password)
        {
            Timestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            PasswordHash = CryptoHelper.GetPasswordHash(password.Trim(), Login, Timestamp);
        }

        public User(string login, string password, string name, int role)
        {
            string loginCandidate = login.Trim();

            if (String.IsNullOrWhiteSpace(loginCandidate))
                throw new ArgumentException("Логін не може бути порожнім");
            if (password.Trim().Length < 6)
                throw new ArgumentException("Пароль не може бути меншим за 6 символів");
            if (SelectFirst("SELECT id FROM Users WHERE login = '" + loginCandidate.Replace("'", "''") + "'").Count > 0)
                throw new ArgumentException("Такий логін вже використовується");

            Id = null;
            Login = loginCandidate;
            GeneratePwdAndTime(password);
            Name = name.Trim();
            Role = (UserRoles) role;
        }

        public static User FindByLogin(string login)
        {
            return new User(login);
        }

        public static User FindById(int? id)
        {
            return new User(id);           
        }

        public static List<User> FindAll()
        {
            List<List<object>> rawUsers = SelectAll("SELECT id, login, password, timestamp, name, role FROM Users");
            List<User> users = new List<User>();

            foreach (var rawUser in rawUsers)
            {
                User user = new User((int)rawUser[0], (string)rawUser[1], (string)rawUser[2], (int)rawUser[3], (string)rawUser[4], (int)rawUser[5]);
                users.Add(user);
            }

            return users;
        }

        public static List<User> FindAllByName(string name)
        {
            List<List<object>> rawUsers = SelectAll("SELECT id, login, password, timestamp, name, role FROM Users WHERE name LIKE '%" + name.Replace("'", "''") + "%'");
            List<User> users = new List<User>();

            foreach (var rawUser in rawUsers)
            {
                User user = new User((int)rawUser[0], (string)rawUser[1], (string)rawUser[2], (int)rawUser[3], (string)rawUser[4], (int)rawUser[5]);
                users.Add(user);
            }

            return users;
        }

        private User(int id, string login, string password, int timestamp, string name, int role)
        {
            Id = id;
            Login = login;
            Timestamp = timestamp;
            PasswordHash = password;
            Name = name;
            Role = (UserRoles)role;
        }

        private User(string login)
        {
            string loginCandidate = login.Trim();

            var currentUser = SelectFirst("SELECT id, password, timestamp, name, role FROM Users WHERE login = '" + loginCandidate.Replace("'", "''") + "'");
            //if (currentUser == null || currentUser.Count == 0)
            if (currentUser.Count == 0)
                throw new ArgumentException("Can't find user with required login", nameof(login));

            Login = loginCandidate;
            Id = (int)currentUser[0];
            PasswordHash = (string)currentUser[1];
            Timestamp = (int)currentUser[2];
            Name = (string)currentUser[3];
            Role = (UserRoles)(int)currentUser[4];
        }

        private User(int? id)
        {
            if (id == null)
                throw new ArgumentException("Id must have a value", nameof(id));

            var currentUser = SelectFirst("SELECT login, password, timestamp, name, role FROM Users WHERE id = '" + id + "'");
            //if (currentUser == null || currentUser.Count == 0)
            if (currentUser.Count == 0)
                throw new ArgumentException("Can't find user with required id", nameof(id));

            Login = (string)currentUser[0];
            Id = id;
            PasswordHash = (string)currentUser[1];
            Timestamp = (int)currentUser[2];
            Name = (string)currentUser[3];
            Role = (UserRoles)(int)currentUser[4];
        }

        public void Delete()
        {
            if (Id != null)
                Execute("DELETE FROM Users WHERE id = '" + Id + "'");
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

        public void Save()
        {
            if (Id == null) // Create
                Execute("INSERT INTO Users (login, password, timestamp, name, role) VALUES ('" + Login.Replace("'", "''") + "', '" + PasswordHash + "', '" + Timestamp + "', '" + Name.Trim().Replace("'", "''") + "', '" + (int)Role + "')");
            else // Update
                Execute("UPDATE Users SET name = '" + Name.Trim().Replace("'", "''") + "', role = '" + (int)Role + "' WHERE id = '" + Id + "'");
        }

        public void ChangePassword(string password)
        {
            if (password.Trim().Length < 6)
                throw new ArgumentException("Пароль не може бути меншим за 6 символів");
            if (Id == null)
                throw new ConstraintException("id is null. We can't UPDATE DB Record while password changing.");
            GeneratePwdAndTime(password);
            Execute("UPDATE Users SET password = '" + PasswordHash + "' WHERE id = '" + Id + "'");
        }
    }
}

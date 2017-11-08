using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public static class CurrentAdminExam
    {
        public static Exam Exam;
    }

    public class Exam: BaseModel
    {
        public enum PassedFilter
        {
            Passed,
            NotPassed,
            All
        }

        private readonly int _passedAtTimestamp;
        private readonly int _userId;

        public int? Id { get; private set; }
        public User User => User.FindById(_userId);
        public string Name { get; set; }
        public string Error { get; set; }
        public bool Passed { get; set; }
        public DateTime PassedAt => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_passedAtTimestamp).ToLocalTime();

        public Exam(int userId, string name, string error, bool passed = false)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Exam name cannot be empty", nameof(name));

            _userId = userId;
            Name = name.Trim();
            Error = error;
            _passedAtTimestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Passed = passed;
        }

        public Exam(User user, string name, string error, bool passed = false)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Exam name cannot be empty", nameof(name));
            if (user?.Id == null)
                throw new ArgumentException("User and user_id cannot be null", nameof(user));

            _userId = user.Id ?? 0;
            Name = name.Trim();
            Error = error;
            _passedAtTimestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Passed = passed;
        }

        private Exam(int id, int userId, string name, string error, int passed, int passedAtTimestamp)
        {
            Id = id;
            _userId = userId;
            Name = name.Trim();
            Error = error;
            _passedAtTimestamp = passedAtTimestamp;
            Passed = passed != 0;
        }

        private Exam(int? id)
        {
            if (id == null)
                throw new ArgumentException("Id must have a value", nameof(id));

            var currentExam = SelectFirst("SELECT id, user_id, name, error_message, passed, passed_at FROM Exams WHERE id = '" + id + "'");
            if (currentExam.Count == 0)
                throw new ArgumentException("Can't find exam with required id", nameof(id));

            Id = id;
            _userId = (int)currentExam[1];
            Name = (string)currentExam[2];
            Error = (string)currentExam[3];
            Passed = (int)currentExam[4] != 0;
            _passedAtTimestamp = (int)currentExam[5];
        }

        public static Exam FindById(int? id)
        {
            return new Exam(id);
        }

        public static List<Exam> Find(DateTime fromDate, DateTime toDate, string examName, string userName, PassedFilter passedFilter)
        {
            bool filterByUser = false;
            bool filterByName = false;
            List<User> users = new List<User>();

            if (!String.IsNullOrWhiteSpace(userName))
            {
                filterByUser = true;
                users = User.FindAllByName(userName);
            }

            if (!String.IsNullOrWhiteSpace(examName))
                filterByName = true;

            int fromTimestamp = (Int32)fromDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            int toTimestamp = (Int32)toDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            string query = "SELECT id, user_id, name, error_message, passed, passed_at FROM Exams WHERE " +
                "passed_at >= '" + fromTimestamp + "' AND passed_at <= '" + toTimestamp + "' ";

            if (filterByName)
                query += "AND name LIKE '%" + examName + "%' ";

            if (filterByUser && users.Count > 0)
            {
                query += "AND (";
                int index = 0;
                foreach (var user in users)
                {
                    if (index > 0)
                        query += " OR ";
                    query += "user_id == '" + user.Id + "'";
                    index++;
                }
                    

                query += ") ";
            }

            if (passedFilter == PassedFilter.NotPassed)
                query += "AND passed == '0' ";
            if (passedFilter == PassedFilter.Passed)
                query += "AND passed == '1' ";

            query += "ORDER BY passed_at DESC";

            List<List<object>> rawExams = SelectAll(query);

            List<Exam> exams = new List<Exam>();

            foreach (var rawExam in rawExams)
            {
                Exam exam = new Exam((int)rawExam[0], (int)rawExam[1], (string)rawExam[2], (string)rawExam[3], (int)rawExam[4], (int)rawExam[5]);
                exams.Add(exam);
            }

            return exams;
        }

        public int Save()
        {
            if (Id == null) // Create
            {
                DbPerference.Instance.Dbconn().Open();
                IDbCommand dbcmd = DbPerference.Instance.Dbconn().CreateCommand();
                dbcmd.CommandText = "INSERT INTO Exams (user_id, name, error_message, passed, passed_at) VALUES ('" +
                                    _userId + "', '" + Name + "', '" + Error + "', '" + (Passed ? "1" : "0") + "', '" +
                                    _passedAtTimestamp + "')";
                dbcmd.ExecuteNonQuery();
                dbcmd.CommandText = "SELECT last_insert_rowid()";
                object i = dbcmd.ExecuteScalar();
                dbcmd.CommandText = "SELECT id FROM Exams WHERE rowid=" + i;
                Id = Int32.Parse(dbcmd.ExecuteScalar().ToString());
                dbcmd.Dispose();
                DbPerference.Instance.Dbconn().Close();
            }
            else //Update
            {
                Execute("UPDATE Exams SET user_id = '" + _userId + ", name = '" + Name.Trim() + ", error_message = '" +
                        Error.Trim() + ", passed = '" + (Passed ? "1" : "0") + ", passed_at = '" + _passedAtTimestamp + 
                        "' WHERE id = '" + Id + "'");
            }

            return Id ?? 0;
        }
    }
}

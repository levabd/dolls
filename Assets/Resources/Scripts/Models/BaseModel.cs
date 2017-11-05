using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public class BaseModel
    {
        protected List<object> SelectFirst(string query)
        {
            try
            {
                List<object> result = new List<object>();

                DbPerference.Instance.Dbconn().Open();
                IDbCommand dbcmd = DbPerference.Instance.Dbconn().CreateCommand();
                dbcmd.CommandText = query;
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var col in Enumerable.Range(0, reader.FieldCount))
                    {
                        if (reader.GetDataTypeName(col) == "TEXT")
                            result.Add(reader.GetString(col));
                        if (reader.GetDataTypeName(col) == "INTEGER")
                            result.Add(reader.GetInt32(col));
                    }
                }
                reader.Close();
                dbcmd.Dispose();
                DbPerference.Instance.Dbconn().Close();

                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError("Oh Crap! " + ex.Message);
                return null;
            }
        }

        protected static List<List<object>> SelectAll(string query)
        {
            try
            {
                List<List<object>> result = new List<List<object>>();

                DbPerference.Instance.Dbconn().Open();
                IDbCommand dbcmd = DbPerference.Instance.Dbconn().CreateCommand();
                dbcmd.CommandText = query;
                IDataReader reader = dbcmd.ExecuteReader();
                while (reader.Read())
                {
                    List<object> row = new List<object>();

                    foreach (var col in Enumerable.Range(0, reader.FieldCount))
                    {
                        if (reader.GetDataTypeName(col) == "TEXT")
                            row.Add(reader.GetString(col));
                        if (reader.GetDataTypeName(col) == "INTEGER")
                            row.Add(reader.GetInt32(col));
                    }

                    result.Add(row);
                }
                reader.Close();
                dbcmd.Dispose();
                DbPerference.Instance.Dbconn().Close();

                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError("Oh Crap! " + ex.Message);
                return null;
            }
        }

        protected bool Execute(string query)
        {
            try
            {
                DbPerference.Instance.Dbconn().Open();
                IDbCommand dbcmd = DbPerference.Instance.Dbconn().CreateCommand();
                dbcmd.CommandText = query;
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                DbPerference.Instance.Dbconn().Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Oh Crap! " + ex.Message);
                return false;
            }
        }

    }

    public class DbPerference : Singleton<DbPerference>
    {
        private DbPerference() { }

        private IDbConnection _dbconn;

        public IDbConnection Dbconn()
        {
            return _dbconn ?? (_dbconn = new SqliteConnection("URI=file:" + Application.dataPath + "/dolls.db"));
        } 
    }
}

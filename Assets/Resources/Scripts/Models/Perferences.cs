using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DB.Models
{
    public class Perferences: BaseModel
    {
        private string _currentLogin;
        private string _currentPasswordHash;

        public string CurrentLogin
        {
            get { return _currentLogin; }
            set
            {
                if (_currentLogin != null && value.Trim() != _currentLogin)
                {
                    string loginCandidate = value.Trim();
                    _currentLogin = loginCandidate;
                }
            }
        }

        public string CurrentPasswordHash
        {
            get { return _currentPasswordHash; }
            set
            {
                if (value.Trim() != _currentPasswordHash)
                {
                    string passwordCandidate = value.Trim();
                    try
                    {
                        User currentUser = new User(_currentLogin);
                        _currentPasswordHash = CryptoHelper.GetPasswordHash(passwordCandidate, CurrentLogin.Trim(), currentUser.Timestamp);
                    }
                    catch (ArgumentException ex)
                    {
                        Debug.Log("Oh Crap! " + ex.Message);
                    }
                }
            }
        }

        public void SaveLogin()
        {
            Execute("UPDATE Perferences SET login = '" + CurrentLogin.Trim() + "', password = '" + CurrentPasswordHash + "' WHERE id = 1");
        }

        public bool LoadLogin()
        {
            List<object> queryResult = SelectFirst("SELECT login, password FROM Perferences WHERE id = 1");
            if (queryResult == null)
                return false;

            _currentLogin = (string) queryResult[0];
            _currentPasswordHash = (string) queryResult[1];

            return true;
        }
    }
}

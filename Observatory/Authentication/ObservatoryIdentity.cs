using System;
using System.Text;
using System.Security.Principal;
using System.Security.Cryptography;


namespace Observatory.Authentication
{
    public class ObservatoryIdentity : IIdentity
    {
        private string _nameValue;
        private bool _authenticatedValue;
        private WindowsBuiltInRole _roleValue;

        public string AuthenticationType
        {
            get { return "Custom Authentication"; }
        }

        public bool IsAuthenticated
        {
            get
            { return _authenticatedValue; }
        }

        public string Name
        {
            get { return _nameValue; }
        }

        public WindowsBuiltInRole Role
        {
            get { return _roleValue; }
        }

        public ObservatoryIdentity(string UserName, string Password)
        {
            if (IsValidNameAndPassword(UserName, Password))
            {
                _nameValue = UserName;
                _authenticatedValue = true;
                _roleValue = WindowsBuiltInRole.Administrator;
            }
            else
            {
                _nameValue = "";
                _authenticatedValue = false;
                _roleValue = WindowsBuiltInRole.Guest;
            }              
        }

        private bool IsValidNameAndPassword(string UserName, string Password)
        {
            string storedHashedPW = GetHashedPassword(UserName);
            string salt = GetSalt(UserName);

            string rawSalted = string.Concat(Password, salt);
            byte[] saltedPWBytes = Encoding.UTF8.GetBytes(rawSalted);
            byte[] hashedPWBytes = SHA256.Create().ComputeHash(saltedPWBytes);
            string hashedPW = Convert.ToBase64String(hashedPWBytes);

            return hashedPW == storedHashedPW;
        }

        private string GetHashedPassword(string UserName)
        {
            return UserName.Trim().ToLower() == "admin" ?
                "guUNHFqLrpGC0uW+581b13pA5NnbxWjiexCy64PfYpw=" : "";
        }

        private string GetSalt(string UserName)
        {
            return UserName.Trim().ToLower() == "admin" ? "Abracadabra" : "";
        }
    }
}

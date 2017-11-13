using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Observatory.Authentication
{
    public class ObservatoryPrincipal : IPrincipal
    {
        private ObservatoryIdentity _identityValue;

        public IIdentity Identity
        {
            get
            {
                return _identityValue;
            }
        }

        public bool IsInRole(string role)
        {
            return role == _identityValue.Role.ToString();
        }

        public ObservatoryPrincipal(string UserName, string Password)
        {
            _identityValue = new ObservatoryIdentity(UserName, Password);
        }
    }
}

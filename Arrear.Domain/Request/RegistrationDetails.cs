using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.Request
{
    public class RegistrationDetails : LoginDetails
    {
        public string UserType { get; set; }
        public RegistrationDetails(string name, string password, string userType) : base(name, password)
        {
            UserType = userType;
        }
    }
}

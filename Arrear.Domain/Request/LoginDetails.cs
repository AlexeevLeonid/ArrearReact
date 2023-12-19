using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.Request
{
    public class LoginDetails
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public LoginDetails(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}

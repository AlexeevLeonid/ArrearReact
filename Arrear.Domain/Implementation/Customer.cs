using Arrear.Domain.AbstractCore;
using Arrear.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.Implementation
{
    public class Customer : User
    {
        public Customer(string name, string password) : base(name, password, UserType.Customer)
        {
        }
    }
}

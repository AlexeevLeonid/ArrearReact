using Arrear.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.AbstractCore
{
    public abstract class Manager : User
    {
        protected Manager(string name, string password, UserType userType) : base(name, password, userType)
        {
        }
    }
}

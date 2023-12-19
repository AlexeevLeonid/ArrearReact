using Arrear.Domain.Enums;
using Arrear.Domain.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.AbstractCore
{
    public abstract class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        [NotMapped]
        public string[] Roles { get; set; }

        public string? AuthenticationType => "JwtBearer";

        public bool IsAuthenticated => true;

        public UserType UserType { get; set; }

        public Collection<Sep> seps { get; set; }

        public User(string name, string password, UserType userType)
        {
            if (name.Length == 0 && name.Length > 20) throw new ArgumentException(nameof(name));
            Name = name;
            Password = password;
            UserType = userType;
            Id = Guid.NewGuid();
            Roles = Array.Empty<string>();
        }

        public static User FactoryMethod(string username, string password, UserType type)
        {
            switch (type)
            {
                case UserType.Customer: return new Customer(username, password);
                case UserType.ManagerFirstWorkshop: return new ManagerFirstWorkshop(username, password);
                case UserType.ManagerSecondWorkshop: return new ManagerSecondWorkshop(username, password);
            }
            throw new ArgumentException("?");
        }
    }
}

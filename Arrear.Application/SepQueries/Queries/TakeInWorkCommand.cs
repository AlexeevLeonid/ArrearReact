using Arrear.Domain.AbstractCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.SepQueries.Queries
{
    public class TakeInWorkCommand : IRequest<string>
    {
        public string IdSep { get; set; }
        public User Manager { get; set; }

        public TakeInWorkCommand(string id, User manager)
        {
            IdSep = id;
            Manager = manager;
        }
    }
}

using Arrear.Domain.AbstractCore;
using Arrear.Domain.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.SepQueries.Queries
{
    public class GetAllManagerSepsQuery : IRequest<List<Sep>>
    {
        public User User { get; set; }
        public GetAllManagerSepsQuery(User manager) 
        {
            User = manager;
        }
    }
}

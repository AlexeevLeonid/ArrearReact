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
    public class GetSepsForManagerQuery : IRequest<List<Sep>>
    {
        public Manager Manager { get; set; }
        public GetSepsForManagerQuery(Manager manager)
        {
            Manager = manager;
        }
    }
}

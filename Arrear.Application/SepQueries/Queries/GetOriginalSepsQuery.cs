using Arrear.Domain.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.SepQueries.Queries
{
    public class GetOriginalSepsQuery : IRequest<List<Sep>>
    {
        public GetOriginalSepsQuery() { }
    }
}

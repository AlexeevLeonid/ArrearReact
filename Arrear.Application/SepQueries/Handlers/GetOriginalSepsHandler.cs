using Arrear.Application.SepQueries.Queries;
using Arrear.Domain.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.SepQueries.Handlers
{
    public class GetOriginalSepsHandler : IRequestHandler<GetOriginalSepsQuery, List<Sep>>
    {

        public GetOriginalSepsHandler() { }
        public async Task<List<Sep>> Handle(GetOriginalSepsQuery request, CancellationToken cancellationToken)
        {
            return new List<Sep>
            {
                new Sep(null, Domain.Enums.Sex.Male, 5, ""),
                new Sep(null, Domain.Enums.Sex.Male, 10, ""),
                new Sep(null, Domain.Enums.Sex.Female, 15, ""),
                new Sep(null, Domain.Enums.Sex.Female, 25, "")
            };
        }
    }
}

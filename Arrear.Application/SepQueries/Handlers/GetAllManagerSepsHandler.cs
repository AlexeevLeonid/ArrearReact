using Arrear.Application.SepQueries.Queries;
using Arrear.Domain.Enums;
using Arrear.Domain.Implementation;
using Arrear.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.SepQueries.Handlers
{
    public class GetAllManagerSepsHandler : IRequestHandler<GetAllManagerSepsQuery, List<Sep>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllManagerSepsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Sep>> Handle(GetAllManagerSepsQuery request, CancellationToken cancellationToken)
        {
            return request.User.seps.ToList();
        }
    }
}

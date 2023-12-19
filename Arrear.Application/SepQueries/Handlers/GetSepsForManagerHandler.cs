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
    public class GetSepsForManagerHandler : IRequestHandler<GetSepsForManagerQuery, List<Sep>>
    {

        private readonly IUnitOfWork _unitOfWork;
        protected SepStatus status;

        private readonly Dictionary<UserType, SepStatus> statusTable = new()
        {
            {UserType.ManagerFirstWorkshop, SepStatus.OrderAssembly },
            {UserType.ManagerSecondWorkshop, SepStatus.FirstWorkshopReady }
        };

        public GetSepsForManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Sep>> Handle(GetSepsForManagerQuery request, CancellationToken cancellationToken)
        {

            return await _unitOfWork.Seps.GetSepsByStatus(statusTable[request.Manager.UserType]);
        }
    }
}

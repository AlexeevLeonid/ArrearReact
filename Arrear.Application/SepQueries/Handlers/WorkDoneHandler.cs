using Arrear.Application.SepQueries.Queries;
using Arrear.Domain.AbstractCore;
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
    public class WorkDoneHandler : IRequestHandler<WorkDoneCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Dictionary<UserType, (SepStatus, SepStatus)> workActions = new Dictionary<UserType, (SepStatus, SepStatus)>()
        {
            {UserType.ManagerFirstWorkshop, (SepStatus.FirstWorkshopInWork, SepStatus.FirstWorkshopReady) },
            {UserType.ManagerSecondWorkshop, (SepStatus.SecondWorkshopInWork, SepStatus.SecondWorkshopReady) }
        };


        public WorkDoneHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<string> Handle(WorkDoneCommand request, CancellationToken cancellationToken)
        {
            if (request.Manager is Customer) return "wrong manager";
            if (!Guid.TryParse(request.IdSep, out Guid guid)) return "wrong id";
            var sep = await _unitOfWork.Seps.GetSepById(guid);
            if (sep.Status != workActions[request.Manager.UserType].Item1) return "wrong status";
            sep.Status = workActions[request.Manager.UserType].Item2;
            sep.ClearManager();
            await _unitOfWork.Seps.PutSep(sep);
            await _unitOfWork.Users.PutUserAsync(request.Manager);
            return "ok";
        }
    }
}

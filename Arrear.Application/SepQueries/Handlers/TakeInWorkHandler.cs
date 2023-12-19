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
    public class TakeInWorkHandler : IRequestHandler<TakeInWorkCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly Dictionary<UserType, (SepStatus, SepStatus)> workActions = new Dictionary<UserType, (SepStatus, SepStatus)>()
        {
            {UserType.ManagerFirstWorkshop, (SepStatus.OrderAssembly, SepStatus.FirstWorkshopInWork) },
            {UserType.ManagerSecondWorkshop, (SepStatus.FirstWorkshopReady, SepStatus.SecondWorkshopInWork) }
        };

        public TakeInWorkHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(TakeInWorkCommand request, CancellationToken cancellationToken)
        {
            if (request.Manager is not Manager manager) return "its a Customer";
            if (!Guid.TryParse(request.IdSep, out Guid guid)) return "wrong id";
            var sep = await _unitOfWork.Seps.GetSepById(guid);
            if (sep.Status != workActions[manager.UserType].Item1) return "wrong status";
            sep.Status = workActions[manager.UserType].Item2;
            sep.SetManager(manager);
            await _unitOfWork.Seps.PutSep(sep);
            await _unitOfWork.Users.PutUserAsync(manager);
            return "ok";
        }
    }
}

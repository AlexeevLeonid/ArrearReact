using Arrear.Domain.Enums;
using Arrear.Domain.Implementation;
using Arrear.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.CreateOrder
{
    public class CreateSepOrderHandler : IRequestHandler<CreateSepOrderCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly Dictionary<string, Sex> stringToSex =
            new Dictionary<string, Sex>()
            {
                {"Male" , Sex.Male },
                {"Female" , Sex.Female }
            };
        public CreateSepOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<string> Handle(CreateSepOrderCommand request, CancellationToken cancellationToken)
        {
            var size = int.Parse(request.Details.Size);
            if (size <= 0 || size >= 30)
            {
                return "wrong size";
            }
            Sex sex;
            if (stringToSex.Keys.Any(x => x == request.Details.Sex))
                sex = stringToSex[request.Details.Sex];
            else return "wrong sex";
            var sep = new Sep(request.Customer, sex, size, request.Details.DeliveryAdress);
            await _unitOfWork.Seps.PostSep(sep);
            return "success";
        }
    }
}

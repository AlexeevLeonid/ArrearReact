using Arrear.Domain.AbstractCore;
using Arrear.Domain.Implementation;
using Arrear.Domain.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Application.CreateOrder
{
    public class CreateSepOrderCommand : IRequest<string>
    {
        public CreateSepOrderDetails Details { get; set; }

        public Customer Customer { get; set; }
        public CreateSepOrderCommand(Customer user, CreateSepOrderDetails details)
        {
            Customer = user;
            this.Details = details;
        }
    }
}

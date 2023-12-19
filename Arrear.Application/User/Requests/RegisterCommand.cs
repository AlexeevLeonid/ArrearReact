using Arrear.Domain.Request;
using MediatR;

namespace Arrear.Application.UserRequests.Requests
{
    public class RegisterCommand : IRequest<Domain.AbstractCore.User>
    {
        public RegistrationDetails RegistrationDetails { get; set; }

        public RegisterCommand(RegistrationDetails registrationDetails)
        {
            RegistrationDetails = registrationDetails;
        }
    }
}

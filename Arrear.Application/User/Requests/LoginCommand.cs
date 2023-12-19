using Arrear.Domain.Request;
using Arrear.Domain.Implementation;
using MediatR;


namespace Arrear.Application.UserRequests.Requests
{
    public class LoginCommand : IRequest<Domain.AbstractCore.User>
    {
        public LoginDetails LoginDetails { get; set; }

        public LoginCommand(LoginDetails loginDetails)
        {
            LoginDetails = loginDetails;
        }
    }
}

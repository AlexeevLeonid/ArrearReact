using Arrear.Application.UserRequests.Requests;
using Arrear.Services;
using MediatR;

namespace Arrear.Application.UserRequests.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Domain.AbstractCore.User>
    {
        private readonly IUserService _userService;
        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Domain.AbstractCore.User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AuthenticateAsync(request.LoginDetails);
            return result;
        }
    }
}

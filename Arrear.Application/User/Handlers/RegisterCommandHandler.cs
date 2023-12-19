using Arrear.Application.UserRequests.Requests;
using Arrear.Services;
using MediatR;

namespace Arrear.Application.UserRequests.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Domain.AbstractCore.User>
    {
        private readonly IUserService _userService;
        public RegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Domain.AbstractCore.User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.RegisterAsync(request.RegistrationDetails) as Domain.AbstractCore.User;
            return user;
        }
    }
}

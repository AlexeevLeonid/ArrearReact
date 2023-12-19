using Arrear.Domain.AbstractCore;
using Arrear.Domain.Request;
using Microsoft.AspNetCore.Http;

namespace Arrear.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(LoginDetails details);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserFromContextAsync(HttpContext context);
        Guid GetUserIdFromContextAsync(HttpContext context);
        Task<IUser> RegisterAsync(RegistrationDetails details);
    }
}
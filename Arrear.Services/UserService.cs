using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Arrear.Domain.AbstractCore;
using Arrear.Domain.Enums;
using Arrear.Domain.Request;
using Arrear.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Arrear.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _rep;
        public UserService(IUnitOfWork rep)
        {
            _rep = rep;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _rep.Users.GetUserAsync(id) ??
                throw new ArgumentException();
        }

        public async Task<User> GetUserFromContextAsync(HttpContext context)
        {
            var id = GetUserIdFromContextAsync(context);
            return await _rep.Users.GetUserAsync(id);
        }
        public Guid GetUserIdFromContextAsync(HttpContext context)
        {
            var claims = context.User.Claims.ToList();
            if (claims.Count == 0)
                throw new ArgumentException();
            return new Guid(claims[0].Value.ToString());
        }

        public async Task<User> AuthenticateAsync(LoginDetails details)
        {
            var user = await _rep.Users.LoginAsync(details) ??
                throw new ArgumentException();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secretyour-256-bit-secretyour-256-bit-secretyour-256-bit-secretyour-256-bit-secret"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
                {
                new Claim("id", user.Id.ToString())
                };
            // Генерируем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),  // действие токена истекает через 30 минут
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            user.Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return user;

        }

        public async Task<IUser> RegisterAsync(RegistrationDetails details)
        {
            UserType type = UserType.Customer;
            switch (details.UserType)
            {
                case "Customer": type = UserType.Customer; break;
                case "ManagerFirstWorkshop": type = UserType.ManagerFirstWorkshop; break;
                case "ManagerSecondWorkshop": type = UserType.ManagerSecondWorkshop; break;
            }
            var user = User.FactoryMethod(
                details.Name,
                details.Password,
                type);
            await _rep.Users.PostUserAsync(user);
            return AuthenticateAsync(details).Result;

        }
    }
}

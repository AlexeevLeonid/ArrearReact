using Arrear.Domain.AbstractCore;
using Arrear.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArrearReact.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IUserService? _userService;
        protected IUserService userService =>
            _userService ??= HttpContext.RequestServices.GetService<IUserService>() ??
            throw new Exception("UserServise is not solved");
        protected IMediator Mediator =>
            HttpContext.RequestServices.GetService<IMediator>() ??
            throw new Exception("Mediator is not solved");


        /// <summary>
        /// Authenticated user from context claims, do not use in AllowAnonymous endpoints
        /// </summary>
        protected User AuthUser =>
            User.Identity != null && !User.Identity.IsAuthenticated ?
            throw new ArgumentException("Is not Authenticated") :
            userService.GetUserFromContextAsync(ControllerContext.HttpContext).Result ??
            throw new ArgumentException("Is authenticated without valid user");

    }
}

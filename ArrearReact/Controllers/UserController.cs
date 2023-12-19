using Arrear.Application.UserRequests.Requests;
using Arrear.Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArrearReact.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : BaseController
    {
        [HttpPost("/authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginDetails details)
        {
            var request = new LoginCommand(details);
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetActualUserSTate()
        {
            var details = new LoginDetails(AuthUser.Name, AuthUser.Password);
            var request = new LoginCommand(details);
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationDetails details)
        {
            var request = new RegisterCommand(details);
            return Ok(await Mediator.Send(request));
        }

    }
}

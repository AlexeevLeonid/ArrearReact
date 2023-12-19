
using Arrear.Application.CreateOrder;
using Arrear.Application.SepQueries.Handlers;
using Arrear.Application.SepQueries.Queries;
using Arrear.Application.UserRequests.Requests;
using Arrear.Domain.AbstractCore;
using Arrear.Domain.Implementation;
using Arrear.Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArrearReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SepController : BaseController
    {
        [HttpGet("default")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDefaultSeps()
        {
            var request = new GetOriginalSepsQuery();
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("toWork")]
        public async Task<IActionResult> GetSepsToWork()
        {
            var request = new GetSepsForManagerQuery(AuthUser as Manager ?? 
                throw new ArgumentException("Not a manager"));
            return Ok(await Mediator.Send(request));
        }


        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromBody] CreateSepOrderDetails details)
        {
            var request = new CreateSepOrderCommand(
                AuthUser as Customer ?? 
                throw new ArgumentException("User is not a Customer"), 
                details);
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("take")]
        public async Task<IActionResult> TakeInWork([FromBody] string id)
        {
            var request = new TakeInWorkCommand(
                id,
                AuthUser as Manager ?? 
                throw new ArgumentException("User is not a Manager"));
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("done")]
        public async Task<IActionResult> WorkDone([FromBody] string id)
        {
            var request = new WorkDoneCommand(id,
                AuthUser as Manager ??
                throw new ArgumentException("User is not a Manager"));
            return Ok(await Mediator.Send(request));
        }
    }
}

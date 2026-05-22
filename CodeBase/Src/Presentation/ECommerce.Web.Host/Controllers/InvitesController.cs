using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Invites.Commands.AcceptInvite;
using Application.Invites.Commands.SendInvite;


namespace ECommerce.Web.Host.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/invites")]
    public class InvitesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SendInvite(SendInviteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new
            {
                Message = "Invite sent successfully"
            });
        }

        [HttpPost("accept")]
        public async Task<ActionResult> AcceptInvite(AcceptInviteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}


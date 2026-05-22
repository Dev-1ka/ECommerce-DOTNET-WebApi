using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Auth.Commands.LoginUser;
using Application.Auth.Commands.RefreshToken;
using Application.Auth.Commands.RegisterUser;
using Application.DTOs;

namespace Inventory.Web.Host.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/auth")]
    [ApiController]

        public class AuthController : ControllerBase
        {
            private readonly IMediator _mediator;

            public AuthController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("register")]
            public async Task<ActionResult<string>> Register([FromBody] RegisterUserCommand command)
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }

            [HttpPost("login")]
            public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUserCommand command)
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }

            [HttpPost("refresh-token")]
            public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenDto dto)
            {
                var result = await _mediator.Send(new RefreshTokenCommand(dto.Token));

                return Ok(result);
            }
        }
 }


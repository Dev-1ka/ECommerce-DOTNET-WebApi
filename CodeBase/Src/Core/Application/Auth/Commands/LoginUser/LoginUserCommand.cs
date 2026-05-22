using MediatR;
using Application.DTOs;

namespace Application.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

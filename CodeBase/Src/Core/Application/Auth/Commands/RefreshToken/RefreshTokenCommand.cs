using MediatR;
using Application.DTOs;

namespace Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponseDto>
    {
        public string Token { get; set; }

        public RefreshTokenCommand(string token)
        {
            Token = token;
        }
    }
}

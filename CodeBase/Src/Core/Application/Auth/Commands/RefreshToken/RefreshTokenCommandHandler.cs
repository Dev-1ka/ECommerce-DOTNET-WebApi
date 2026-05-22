using Application.Auth.Commands.LoginUser;
using Application.Interfaces;
using Application.DTOs;
using MediatR;

namespace Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly RefreshTokenCommandValidator _validator;
        public RefreshTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
            _validator = new RefreshTokenCommandValidator();
        }

        public async Task<AuthResponseDto> Handle( RefreshTokenCommand request,CancellationToken cancellationToken)
        {
            var result = await _identityService.RefreshTokenAsync(request.Token);
            return result;
        }
    }
}

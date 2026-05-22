using Application.Interfaces;
using MediatR;
using FluentValidation;
using Application.DTOs;

namespace Application.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly LoginUserCommandValidator _validator;
        private readonly IJwtTokenService _jwtService;
        public LoginUserCommandHandler(IIdentityService identityService, IJwtTokenService jwtService)
        {
            _identityService = identityService;
            _validator = new LoginUserCommandValidator();
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var response = await _identityService.LoginAsync(request.Email, request.Password);

            if (response == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            return response;
        }
    }
}

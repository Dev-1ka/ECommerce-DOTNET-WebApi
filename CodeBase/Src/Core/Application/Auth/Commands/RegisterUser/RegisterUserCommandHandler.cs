using Application.Interfaces;
using MediatR;
using FluentValidation;


namespace Application.Auth.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly RegisterUserCommandValidator _validator;
        public RegisterUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
            _validator = new RegisterUserCommandValidator();
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //Validation
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);


            //Call Identity Service
            var (succeeded, userId) = await _identityService.RegisterAsync(
                request.Email,
                request.Password,
                request.FullName);

            if (!succeeded) throw new Exception(userId);

            return $"User {userId} created successfully";
        }
    }
}

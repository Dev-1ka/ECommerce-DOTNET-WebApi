using FluentValidation;

namespace Application.Products.Commands.CreateProduct
{
    
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0).NotEmpty();
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).NotEmpty();
        }
    }
}

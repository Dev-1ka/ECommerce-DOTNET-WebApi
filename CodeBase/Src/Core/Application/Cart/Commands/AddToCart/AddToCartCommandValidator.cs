using Application.Cart.Commands.AddToCart;
using FluentValidation;

namespace Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
           
      
     
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required");

       
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}

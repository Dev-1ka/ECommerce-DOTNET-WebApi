using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Commands.RemoveCartItem
{
    public class RemoveCartItemCommandValidator : AbstractValidator<RemoveCartItemCommand>
    {
        public RemoveCartItemCommandValidator()
        {
            RuleFor(x => x.CartItemId)
                .NotEmpty().WithMessage("CartItemId is required");
        }
    }
}

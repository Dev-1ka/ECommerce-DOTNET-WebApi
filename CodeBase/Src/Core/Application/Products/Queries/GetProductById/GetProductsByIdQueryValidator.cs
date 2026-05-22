
using FluentValidation;

namespace Application.Products.Queries.GetProductById
{
    public class GetProductsByIdQueryValidator : AbstractValidator<GetProductsByIdQuery>
    {
        public GetProductsByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

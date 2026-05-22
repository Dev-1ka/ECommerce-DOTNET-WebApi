using Application.DTOs;
using MediatR;

namespace Application.Products.Queries.GetProductById
{
    public record GetProductsByIdQuery(Guid Id) : IRequest<ProductDto>;
}

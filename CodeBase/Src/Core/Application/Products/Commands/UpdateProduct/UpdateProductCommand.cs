using Application.DTOs;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name,string Description,decimal Price,int Stock) : IRequest<string>;
}

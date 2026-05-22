using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductById
{
    public class GetProductsByIdQueryHandler
        : IRequestHandler<GetProductsByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _context;

        public GetProductsByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(
            GetProductsByIdQuery request,
            CancellationToken cancellationToken)
        {
       
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id && !p.IsDeleted, cancellationToken);

            if (product == null)
                throw new Exception("Product not found");

    
            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(i => i.ProductId == product.Id, cancellationToken);

       
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,

           
                Stock = inventory?.AvailableStock ?? 0
            };
        }
    }
}
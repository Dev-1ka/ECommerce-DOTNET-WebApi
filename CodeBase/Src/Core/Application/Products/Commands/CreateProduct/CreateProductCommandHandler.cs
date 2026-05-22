using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            _validator = new CreateProductCommandValidator();
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Products
                .AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (exists)
                throw new ArgumentException("Product with same name already exists.");

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            var inventory = new ProductInventory
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                AvailableStock = request.Stock,  
                ReservedStock = 0,
                LowStockThreshold = 5
            };

            _context.Inventory.Add(inventory);

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
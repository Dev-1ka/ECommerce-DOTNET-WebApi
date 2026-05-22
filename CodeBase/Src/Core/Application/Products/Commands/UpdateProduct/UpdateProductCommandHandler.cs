using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly UpdateProductCommandValidator _validator;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            _validator = new UpdateProductCommandValidator();
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id && !p.IsDeleted, cancellationToken);

            if (product == null)
                return "Product not found";

         
            var exists = await _context.Products
                .AnyAsync(p => p.Name == request.Name && p.Id != request.Id, cancellationToken);

            if (exists)
                return "Another product with the same name already exists";

          
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

          

            await _context.SaveChangesAsync(cancellationToken);

            return "Product updated successfully";
        }
    }
}
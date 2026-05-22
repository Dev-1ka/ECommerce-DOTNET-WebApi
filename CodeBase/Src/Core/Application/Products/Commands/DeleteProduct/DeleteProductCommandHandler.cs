using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly DeleteProductCommandValidator _validator;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            _validator = new DeleteProductCommandValidator();

        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);

            if (product == null)
                throw new Exception("Product not found");

           
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return "Product deleted successfully";
        }
    }
}

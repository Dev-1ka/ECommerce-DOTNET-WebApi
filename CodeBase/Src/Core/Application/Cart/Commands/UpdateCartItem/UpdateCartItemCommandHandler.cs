using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cart.Commands.UpdateCartItem
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCartItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
         
            if (request.Quantity <= 0)
                return "Quantity must be greater than zero";

        
            var item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.Id == request.CartItemId, cancellationToken);

            if (item == null)
                return "Cart item not found";

       
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == item.ProductId && !p.IsDeleted, cancellationToken);

            if (product == null)
                return "Product no longer exists";

          
            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(i => i.ProductId == item.ProductId, cancellationToken);

            if (inventory == null)
                return "Inventory not found";

           
            if (inventory.AvailableStock <= 0)
                return "Product is out of stock";

            if (request.Quantity > inventory.AvailableStock)
                return "Requested quantity exceeds available stock";

            item.Quantity = request.Quantity;

         
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return $"Database error: {ex.InnerException?.Message ?? ex.Message}";
            }

            return "Cart updated successfully";
        }
    }
}
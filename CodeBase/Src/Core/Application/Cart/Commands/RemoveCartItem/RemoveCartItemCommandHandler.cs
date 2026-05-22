using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cart.Commands.RemoveCartItem
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public RemoveCartItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
    
            if (request.CartItemId == Guid.Empty)
                return "Invalid cart item id";

     
            var item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.Id == request.CartItemId, cancellationToken);

            if (item == null)
                return "Cart item not found";

           
            if (item.IsDeleted)
                return "Item already removed";

        
            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(i => i.ProductId == item.ProductId, cancellationToken);

            if (inventory != null)
            {
                inventory.AvailableStock += item.Quantity;
                inventory.ReservedStock -= item.Quantity;

                if (inventory.ReservedStock < 0)
                    inventory.ReservedStock = 0;
            }

            
            item.IsDeleted = true;
            item.DeletedAt = DateTime.UtcNow;

    
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return $"Database error: {ex.InnerException?.Message ?? ex.Message}";
            }

            return "Item removed from cart successfully";
        }
    }
}
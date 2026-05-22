using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public AddToCartCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
             _currentUserService = currentUserService;
        }

        public async Task<string> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
        
            if (request.Quantity <= 0)
                return "Quantity must be greater than zero";

     
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId && !p.IsDeleted, cancellationToken);

            if (product == null)
                return "Product does not exist";

     
            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(i => i.ProductId == request.ProductId, cancellationToken);

            if (inventory == null)
                return "Inventory not found";

            if (inventory.AvailableStock <= 0)
                return "Product is out of stock";

            if (request.Quantity > inventory.AvailableStock)
                return "Requested quantity exceeds available stock";

            var userId = _currentUserService.UserId;
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                _context.Carts.Add(cart);
            }

         
            var existingItem = await _context.CartItems
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(i =>
                    i.CartId == cart.Id &&
                    i.ProductId == request.ProductId,
                    cancellationToken);

            if (existingItem != null)
            {
                if (existingItem.IsDeleted)
                {
            
                    existingItem.IsDeleted = false;
                    existingItem.DeletedAt = null;
                    existingItem.Quantity = request.Quantity;
                }
                else
                {
                    if (existingItem.Quantity + request.Quantity > inventory.AvailableStock)
                        return "Cannot add more items than available stock";

                    existingItem.Quantity += request.Quantity;
                }
            }
            else
            {

                _context.CartItems.Add(new ShoppingCartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    CartId = cart.Id, 
                    Quantity = request.Quantity
                });
            }

            
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return "Cart was updated elsewhere. Please try again";
            }
            catch (Exception ex)
            {
                return $"Database error: {ex.InnerException?.Message ?? ex.Message}";
            }

            return "Item added to cart successfully";
        }
    }
}
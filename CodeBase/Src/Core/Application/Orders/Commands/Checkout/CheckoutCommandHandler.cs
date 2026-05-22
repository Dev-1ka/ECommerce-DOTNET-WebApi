using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CheckoutCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CheckoutCommand request,
            CancellationToken cancellationToken)
            {
            using var transaction =
                await (_context as DbContext)!
                .Database
                .BeginTransactionAsync(cancellationToken);

            try
            {
         
                var cart = await _context.Carts
                    .Include(c => c.Items.Where(i => !i.IsDeleted))
                        .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(
                        c => c.UserId == request.UserId,
                        cancellationToken);

        
                if (cart == null || !cart.Items.Any())
                    throw new Exception("Cart is empty");

           
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                decimal total = 0;

             
                foreach (var item in cart.Items)
                {
                
                    var inventory = await _context.Inventory
                        .FirstOrDefaultAsync(
                            i => i.ProductId == item.ProductId,
                            cancellationToken);

                    if (inventory == null)
                        throw new Exception(
                            $"{item.Product.Name} inventory not found");

               
                    if (inventory.AvailableStock < item.Quantity)
                        throw new Exception(
                            $"Only {inventory.AvailableStock} items available for {item.Product.Name}");

             
                    inventory.AvailableStock -= item.Quantity;
                    inventory.ReservedStock += item.Quantity;

               
                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };

                    order.Items.Add(orderItem);

                    total += item.Quantity * item.Product.Price;
                }

         
                order.TotalAmount = total;

              
                _context.Orders.Add(order);

         
                foreach (var item in cart.Items)
                {
                    item.IsDeleted = true;
                    item.DeletedAt = DateTime.UtcNow;
                }

               
                await _context.SaveChangesAsync(cancellationToken);

         
                await transaction.CommitAsync(cancellationToken);

                return order.Id;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
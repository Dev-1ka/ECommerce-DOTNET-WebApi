using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.PayOrder
{
 public class PayOrderCommandHandler
        : IRequestHandler<PayOrderCommand, string>
        {
            private readonly IApplicationDbContext _context;

            public PayOrderCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(
                PayOrderCommand request,
                CancellationToken ct)
            {
                using var transaction =
                    await (_context as DbContext)!
                    .Database
                    .BeginTransactionAsync(ct);

                try
                {
               
                    var order = await _context.Orders
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(
                            o => o.Id == request.OrderId &&
                                 o.UserId == request.UserId,
                            ct);

                    if (order == null)
                        return "Order not found";

                   
                    if (order.Status == OrderStatus.Paid)
                        return "Order already paid";

                    if (order.Status != OrderStatus.Pending)
                        return "Invalid order state";

                
                    foreach (var item in order.Items)
                    {
                        var inventory = await _context.Inventory
                            .FirstOrDefaultAsync(
                                i => i.ProductId == item.ProductId,
                                ct);

                        if (inventory == null)
                            return "Inventory not found";

                        if (inventory.ReservedStock < item.Quantity)
                            return "Reserved stock mismatch";

          
                        inventory.ReservedStock -= item.Quantity;
                    }

               
                    order.Status = OrderStatus.Paid;
                    order.PaidAt = DateTime.UtcNow;

                    order.ShippedAt = DateTime.UtcNow.AddMinutes(30);

                    order.DeliveredAt = DateTime.UtcNow.AddHours(1);
                await _context.SaveChangesAsync(ct);

                 
                    await transaction.CommitAsync(ct);

                    return "Payment successful";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    return ex.Message;
                }
            }
        }
}
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.ShipOrder
{
    public class ShipOrderCommandHandler
        : IRequestHandler<ShipOrderCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public ShipOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(
            ShipOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(
                    o => o.Id == request.OrderId,
                    cancellationToken);

            if (order == null)
                return "Order not found";

            if (order.Status != OrderStatus.Paid)
                return "Only paid orders can be shipped";

            order.Status = OrderStatus.Shipped;
            order.ShippedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return "Order shipped successfully";
        }
    }
}

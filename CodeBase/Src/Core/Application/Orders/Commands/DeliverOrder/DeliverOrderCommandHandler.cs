using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.DeliverOrder
{
    public class DeliverOrderCommandHandler
        : IRequestHandler<DeliverOrderCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeliverOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(
            DeliverOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(
                    o => o.Id == request.OrderId,
                    cancellationToken);

            if (order == null)
                return "Order not found";

            if (order.Status != OrderStatus.Shipped)
                return "Only shipped orders can be delivered";

            order.Status = OrderStatus.Delivered;
            order.DeliveredAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return "Order delivered successfully";
        }
    }
}

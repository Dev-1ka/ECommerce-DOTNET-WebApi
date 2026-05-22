using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetMyOrders
{
    public class GetMyOrdersQueryHandler
        : IRequestHandler<GetMyOrdersQuery,
            List<OrderResponseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetMyOrdersQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderResponseDto>> Handle(
            GetMyOrdersQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Where(o => o.UserId == request.UserId)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OrderResponseDto
                {
                    OrderId = o.Id,
                    TotalAmount = o.TotalAmount,
                    Status =

                    o.Status == OrderStatus.Pending

                        ? "Pending"

                    : o.Status == OrderStatus.Paid &&
                      o.DeliveredAt <= DateTime.UtcNow

                        ? "Delivered"

                    : o.Status == OrderStatus.Paid &&
                      o.ShippedAt <= DateTime.UtcNow

                        ? "Shipped"

                    : "Paid",
                    CreatedAt = o.CreatedAt,
                    PaidAt = o.PaidAt,
                    ShippedAt = o.ShippedAt,
                    DeliveredAt = o.DeliveredAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}

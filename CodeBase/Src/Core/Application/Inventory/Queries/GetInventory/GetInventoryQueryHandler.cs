using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Queries.GetInventory
{
    public class GetInventoryHandler : IRequestHandler<GetInventoryQuery, List<InventoryResponseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetInventoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryResponseDto>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Inventory
            .Select(i => new InventoryResponseDto
            {
                ProductId = i.ProductId,
                AvailableStock = i.AvailableStock,
                ReservedStock = i.ReservedStock,
                LowStockThreshold = i.LowStockThreshold,
                IsLowStock = i.AvailableStock < i.LowStockThreshold
            })
            .ToListAsync(cancellationToken);
        }
    }
}

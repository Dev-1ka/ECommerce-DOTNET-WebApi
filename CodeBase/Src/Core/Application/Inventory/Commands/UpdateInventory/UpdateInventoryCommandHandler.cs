using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Commands.UpdateInventory
{
    public class UpdateInventoryHandler : IRequestHandler<UpdateInventoryCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInventoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _context.Inventory
    .FirstOrDefaultAsync(i => i.ProductId == request.ProductId, cancellationToken);

            if (inventory == null)
                return false;

            inventory.AvailableStock = request.AvailableStock;
            inventory.LowStockThreshold = request.LowStockThreshold;
            inventory.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

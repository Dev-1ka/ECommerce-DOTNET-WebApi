using MediatR;


namespace Application.Inventory.Commands.UpdateInventory
{
    public class UpdateInventoryCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public int AvailableStock { get; set; }
        public int LowStockThreshold { get; set; }
    }
}

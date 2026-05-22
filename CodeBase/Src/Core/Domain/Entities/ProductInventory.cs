

namespace Domain.Entities
{
    public class ProductInventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public int AvailableStock { get; set; }
        public int ReservedStock { get; set; }

        public int LowStockThreshold { get; set; } = 5;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}

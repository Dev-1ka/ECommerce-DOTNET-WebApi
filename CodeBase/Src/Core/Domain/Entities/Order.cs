using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? PaidAt { get; set; }
        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    }
}

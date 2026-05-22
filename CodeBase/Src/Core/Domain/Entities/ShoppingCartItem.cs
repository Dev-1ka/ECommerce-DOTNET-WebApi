using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Guid CartId { get; set; }
        public ShoppingCart Cart { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}

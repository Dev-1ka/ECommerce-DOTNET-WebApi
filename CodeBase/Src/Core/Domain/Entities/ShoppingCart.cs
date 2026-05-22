using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new();

    }
}

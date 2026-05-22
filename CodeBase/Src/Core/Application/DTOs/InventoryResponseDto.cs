using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InventoryResponseDto
    {
        public Guid ProductId { get; set; }
        public int AvailableStock { get; set; }
        public int ReservedStock { get; set; }
        public int LowStockThreshold { get; set; }
        public bool IsLowStock { get; set; }
    }
}

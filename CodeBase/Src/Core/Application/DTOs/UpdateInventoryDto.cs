using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateInventoryDto
    {
        public int AvailableStock { get; set; }
        public int LowStockThreshold { get; set; }
    }
}

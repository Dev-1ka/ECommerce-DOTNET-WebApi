using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.ShipOrder
{
    public class ShipOrderCommand : IRequest<string>
    {
        public Guid OrderId { get; set; }
    }
}

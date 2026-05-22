using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.PayOrder
{
    public class PayOrderCommand : IRequest<string>
    {
        public Guid OrderId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}

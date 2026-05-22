using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.Checkout
{
    public class CheckoutCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
    }
}

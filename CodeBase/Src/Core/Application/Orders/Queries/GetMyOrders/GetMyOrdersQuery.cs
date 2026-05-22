using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries.GetMyOrders
{
    public class GetMyOrdersQuery
        : IRequest<List<OrderResponseDto>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}

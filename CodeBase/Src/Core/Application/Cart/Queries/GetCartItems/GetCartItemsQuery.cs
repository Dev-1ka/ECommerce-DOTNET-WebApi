using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Cart.Queries.GetCartItems
{
    public class GetCartItemsQuery : IRequest<CartResponseDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
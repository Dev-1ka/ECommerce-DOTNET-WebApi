using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cart.Queries.GetCartItems
{
    public class GetCartItemsQueryHandler
        : IRequestHandler<GetCartItemsQuery, CartResponseDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCartItemsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartResponseDto> Handle(
            GetCartItemsQuery request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.Items.Where(i => !i.IsDeleted))
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(
                    c => c.UserId == request.UserId,
                    cancellationToken);

            if (cart == null)
            {
                return new CartResponseDto
                {
                    CartId = Guid.Empty,
                    Items = new List<CartItemResponseDto>()
                };
            }

            return new CartResponseDto
            {
                CartId = cart.Id,

                Items = cart.Items.Select(i => new CartItemResponseDto
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
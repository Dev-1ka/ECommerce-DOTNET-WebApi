using MediatR;


namespace Application.Cart.Commands.UpdateCartItem
{
    public class UpdateCartItemCommand : IRequest<string>
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}

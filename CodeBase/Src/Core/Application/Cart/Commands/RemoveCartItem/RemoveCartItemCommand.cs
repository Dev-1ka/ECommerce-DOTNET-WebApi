using MediatR;


namespace Application.Cart.Commands.RemoveCartItem
{
    public class RemoveCartItemCommand : IRequest<string>
    {
        public Guid CartItemId { get; set; }
    }
}

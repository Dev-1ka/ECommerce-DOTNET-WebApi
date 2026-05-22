using MediatR;


namespace Application.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<string>
    {
  
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

using Application.Interfaces;
using Application.Orders.Commands.Checkout;
using Application.Orders.Commands.PayOrder;
using Application.Orders.Commands.ShipOrder;
using Application.Orders.Commands.DeliverOrder;
using Application.Orders.Queries.GetMyOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Host.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public OrdersController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout()
        {
            var orderId = await _mediator.Send(
                new CheckoutCommand
                {
                    UserId = _currentUserService.UserId
                });

            return Ok(new
            {
                Message = "Checkout successful",
                OrderId = orderId
            });
        }

        [HttpPost("{id}/pay")]
        public async Task<ActionResult> Pay(Guid id)
        {
            var result = await _mediator.Send(
                new PayOrderCommand
                {
                    OrderId = id,
                    UserId = _currentUserService.UserId
                });

            return Ok(new
            {
                Message = result
            });
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult> MyOrders()
        {
            var orders = await _mediator.Send(
                new GetMyOrdersQuery
                {
                    UserId = _currentUserService.UserId
                });

            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/ship")]
        public async Task<ActionResult> Ship(Guid id)
        {
            var result = await _mediator.Send(
                new ShipOrderCommand
                {
                    OrderId = id
                });

            return Ok(new
            {
                Message = result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/deliver")]
        public async Task<ActionResult> Deliver(Guid id)
        {
            var result = await _mediator.Send(
                new DeliverOrderCommand
                {
                    OrderId = id
                });

            return Ok(new
            {
                Message = result
            });
        }
    }
}
using Application.Cart.Commands.AddToCart;
using Application.Cart.Commands.RemoveCartItem;
using Application.Cart.Commands.UpdateCartItem;
using Application.Cart.Queries.GetCartItems;
using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize(Roles = "User")]
[ApiController]
[Route("api/v1/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public CartController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
    {
        if (dto == null || dto.Quantity <= 0)
            return BadRequest("Invalid input");

        var message = await _mediator.Send(new AddToCartCommand
        {
           
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        });

        return Ok(new { message });
    }

    [HttpPut("update")]
    public async Task<ActionResult> Update([FromBody] UpdateCartItemDto dto)
    {
        if (dto == null || dto.Quantity <= 0)
            return BadRequest("Invalid input");

        var message = await _mediator.Send(new UpdateCartItemCommand
        {
            CartItemId = dto.CartItemId,
            Quantity = dto.Quantity
        });

        return Ok(new { message });
    }

    [HttpDelete("remove/{id}")]
    public async Task<ActionResult> Remove(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid cart item id");

        var message = await _mediator.Send(new RemoveCartItemCommand
        {
            CartItemId = id
        });

        return Ok(new { message });
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var cart = await _mediator.Send(new GetCartItemsQuery
        {
            UserId = _currentUserService.UserId
        });

        return Ok(cart);
    }

   
}
using Application.DTOs;
using Application.Interfaces;
using Application.Inventory.Commands.UpdateInventory;
using Application.Inventory.Queries.GetInventory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Host.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/v1/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,InventoryManager")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetInventoryQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin,InventoryManager")]
        [HttpPut("{productId}")]
      
        public async Task<ActionResult> Update(Guid productId, UpdateInventoryDto dto)
        {
            var command = new UpdateInventoryCommand
            {
                ProductId = productId,
                AvailableStock = dto.AvailableStock,
                LowStockThreshold = dto.LowStockThreshold
            };

            var result = await _mediator.Send(command);

            if (!result) return NotFound();

            return Ok("Updated");
        }
    }
}
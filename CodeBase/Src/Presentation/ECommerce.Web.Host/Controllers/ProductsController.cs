using Application.Common.Pagination;
using Application.DTOs;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetAllProducts;
using Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Host.Controllers
{
    [Authorize]
    [Route("api/v1/products")]
    [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly IMediator _mediator;
           
            public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
            {
                _mediator = mediator;
                
            }
        [Authorize(Roles = "Admin,ProductManager")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand command)
        {
                var productId = await _mediator.Send(command);
                return Ok(new
                {
                    Message = "Product created successfully",
                    ProductId = productId
                });
        }

        [Authorize(Roles ="User,Admin,ProductManager")]
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] PagedRequest request)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(request));
            return Ok(result);
        }

        [Authorize(Roles = "User,Admin,ProductManager")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductsByIdQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Admin,ProductManager")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, string name,string description,decimal price,int stock)
        {
            var message = await _mediator.Send(new UpdateProductCommand(id,name,description,price,stock));
            return Ok(new
            {
                Message = message
            });
        }

        [Authorize(Roles = "Admin,ProductManager")]
        [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {
                var message = await _mediator.Send(new DeleteProductCommand(id));

                return Ok(new
                {
                    Message = message
                });
        }}}


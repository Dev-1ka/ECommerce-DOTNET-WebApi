using Application.Common.Pagination;
using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler
        : IRequestHandler<GetAllProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<ProductDto>> Handle(
    GetAllProductsQuery request,
    CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.Request.Search));
            }

            var totalRecords =
                await query.CountAsync(cancellationToken);

            var data = await query

                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize
                )

                .Take(request.Request.PageSize)

                .GroupJoin(

                    _context.Inventory,

                    product => product.Id,

                    inventory => inventory.ProductId,

                    (product, inventories) =>
                        new ProductDto
                        {
                            Id = product.Id,

                            Name = product.Name,

                            Price = product.Price,

                            Stock = inventories
                                .Select(i =>
                                    i.AvailableStock)
                                .FirstOrDefault(),

                            Description = product.Description,

                            ImageUrl = product.ImageUrl
                        }
                )

                .ToListAsync(cancellationToken);

            return new PagedResponse<ProductDto>
            {
                Data = data,

                PageNumber =
                    request.Request.PageNumber,

                PageSize =
                    request.Request.PageSize,

                TotalRecords =
                    totalRecords,

                TotalPages =
                    (int)Math.Ceiling(
                        totalRecords /
                        (double)request.Request.PageSize
                    )
            };
        }
    }
}
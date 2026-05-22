using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery(PagedRequest Request)
    : IRequest<PagedResponse<ProductDto>>;
}

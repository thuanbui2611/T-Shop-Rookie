using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Product.QueryModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<(List<ProductResponseModel>, PaginationMetaData)>
    {
        public ProductQuery ProductQuery { get; set; }
        public PaginationRequestModel Pagination { get; set; }
    }
}

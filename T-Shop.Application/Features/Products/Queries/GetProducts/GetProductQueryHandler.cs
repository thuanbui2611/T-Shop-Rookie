using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductResponseModel>>
    {
        private readonly IMapper _mapper;

        private readonly IProductReviewQueries _reviewQueries;
        private readonly IProductQueries _productQueries;
        public GetProductQueryHandler(IProductQueries productQueries, IMapper mapper, IProductReviewQueries reviewQueries)
        {
            _productQueries = productQueries;
            _mapper = mapper;
            _reviewQueries = reviewQueries;
        }

        public async Task<List<ProductResponseModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productQueries.GetAllProductsAsync();
            var result = _mapper.Map<List<ProductResponseModel>>(products);
            return result;
        }
    }
}

using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductDto>>
    {
        private readonly IProductQueries _productRepository;
        private readonly IMapper _mapper;
        public GetProductQueryHandler(IProductQueries productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync();
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }
    }
}

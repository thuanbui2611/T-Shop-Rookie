using AutoMapper;
using MediatR;
using T_Shop.Domain.Entity.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Features.Products.Queries.GetProductsById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductQueries _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IMapper mapper, IProductQueries productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.productId);
            if (product == null)
            {
                throw new BadRequestException(message: "Product not found");
            }
            var result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}

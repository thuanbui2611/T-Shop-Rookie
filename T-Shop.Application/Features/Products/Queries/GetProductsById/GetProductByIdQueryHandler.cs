using AutoMapper;
using MediatR;
using T_Shop.Application.Features.Products.ViewModels;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Queries.GetProductsById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDtos>
    {
        private readonly IProductQueries _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IMapper mapper, IProductQueries productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductDtos> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetByIdAsync(request.productId);
            if (product == null)
            {
                //throw new StatusCodeException(message: "Product not found", statusCode: StatusCodes.Status404NotFound);
            }
            var result = _mapper.Map<ProductDtos>(product);
            return result;
        }
    }
}

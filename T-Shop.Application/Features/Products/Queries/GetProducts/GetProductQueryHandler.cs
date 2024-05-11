using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Shop.Application.Features.Products.ViewModels;
using T_Shop.Domain.Repository;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductDtos>>
    {
        private readonly IProductQueries _productRepository;
        private readonly IMapper _mapper;
        public GetProductQueryHandler(IProductQueries productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDtos>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync();
            var result = _mapper.Map<List<ProductDtos>>(products);
            return result;
        }
    }
}

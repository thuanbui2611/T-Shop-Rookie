using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Color> _colorRepository;
        private readonly IGenericRepository<Model> _modelRepository;

        public CreateProductCommandHandler(IMapper mapper, IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _modelRepository = _unitOfWork.GetBaseRepo<Model>();
            _colorRepository = _unitOfWork.GetBaseRepo<Color>();
        }

        public async Task<ProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var color = await _colorRepository.GetById(request.ColorID);
            if (color == null)
            {
                throw new NotFoundException("Color is not found");
            }
            var model = await _modelRepository.GetById(request.ModelID);
            if (model == null)
            {
                throw new NotFoundException("Model is not found");
            }

            var newProduct = _mapper.Map<Product>(request);

            newProduct.IsOnStock = true;
            newProduct.CreatedAt = DateTime.Now;

            _productRepository.Add(newProduct);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<ProductResponseModel>(newProduct);
            return result;
        }
    }
}

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
        private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
        private readonly IModelQueries _modelQueries;
        private readonly IGenericRepository<TypeProduct> _typeRepository;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IModelQueries modelQueries)
        {
            _mapper = mapper;
            _productRepository = unitOfWork.GetBaseRepo<Product>();
            _unitOfWork = unitOfWork;
            _colorRepository = _unitOfWork.GetBaseRepo<Domain.Entity.Color>();
            _typeRepository = _unitOfWork.GetBaseRepo<TypeProduct>();
            _modelQueries = modelQueries;
        }

        public async Task<ProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var color = await _colorRepository.GetById(request.ColorID);
            if (color == null)
            {
                throw new NotFoundException("Color is not found");
            }
            var model = await _modelQueries.GetModelByIdAsync(request.ModelID);
            if (model == null)
            {
                throw new NotFoundException("Model is not found");
            }
            var type = await _typeRepository.GetById(request.TypeID);
            if (type == null)
            {
                throw new NotFoundException("Type is not found");
            }

            var newProduct = _mapper.Map<Product>(request);

            _productRepository.Add(newProduct);
            await _unitOfWork.CompleteAsync();

            newProduct.Model = model;
            newProduct.Type = type;
            newProduct.Color = color;
            var result = _mapper.Map<ProductResponseModel>(newProduct);
            return result;
        }
    }
}

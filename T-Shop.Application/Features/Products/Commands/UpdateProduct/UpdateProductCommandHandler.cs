using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
        private readonly IGenericRepository<Model> _modelRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TypeProduct> _typeRepository;


        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
            _unitOfWork = unitOfWork;
            _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
            _modelRepository = unitOfWork.GetBaseRepo<Model>();
            _productRepository = unitOfWork.GetBaseRepo<Product>();
        }

        public async Task<ProductResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product is not found");
            }
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
            var type = await _typeRepository.GetById(request.TypeID);
            if (type == null)
            {
                throw new NotFoundException("Type is not found");
            }
            var productUpdate = _mapper.Map<Product>(request);
            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            productUpdate.Color = color;
            productUpdate.Model = model;
            productUpdate.Type = type;
            var result = _mapper.Map<ProductResponseModel>(productUpdate);
            return result;
        }
    }
}

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

        public UpdateProductCommandHandler(IMapper mapper, IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
            _unitOfWork = unitOfWork;
            _modelRepository = unitOfWork.GetBaseRepo<Model>();
            _productRepository = unitOfWork.GetBaseRepo<Product>();
        }

        public async Task<ProductResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
            var productUpdate = _mapper.Map<Product>(request);
            _productRepository.Update(productUpdate);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<ProductResponseModel>(productUpdate);
            return result;
        }
    }
}

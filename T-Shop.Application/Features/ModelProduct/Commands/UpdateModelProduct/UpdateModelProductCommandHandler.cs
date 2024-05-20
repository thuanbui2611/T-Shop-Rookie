using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Commands.UpdateModelProduct;
public class UpdateModelProductCommandHandler : IRequestHandler<UpdateModelProductCommand, ModelProductResponseModel>
{
    private readonly IGenericRepository<Model> _modelRepository;
    private readonly IModelQueries _modelQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBrandQueries _brandQueries;

    public UpdateModelProductCommandHandler(IModelQueries modelQueries, IUnitOfWork unitOfWork, IMapper mapper, IBrandQueries brandQueries)
    {
        _modelQueries = modelQueries;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _modelRepository = _unitOfWork.GetBaseRepo<Model>();
        _brandQueries = brandQueries;
    }

    public async Task<ModelProductResponseModel> Handle(UpdateModelProductCommand request, CancellationToken cancellationToken)
    {
        var existedModel = await _modelQueries.GetModelsByNameAsync(request.Name);
        if (existedModel != null)
        {
            foreach (var model in existedModel)
            {
                if (model.Year == request.Year)
                {
                    throw new ConflictException($"The model '{model.Name + " - " + model.Year}' is existed");
                }
            }
        }
        var brand = await _brandQueries.GetBrandByIdAsync(request.BrandID);
        if (brand == null) throw new NotFoundException("Brand not found");

        var modelToUpdated = _mapper.Map<Model>(request);
        _modelRepository.Update(modelToUpdated);
        await _unitOfWork.CompleteAsync();

        modelToUpdated.Brand = new Domain.Entity.Brand()
        {
            Id = brand.Id,
            Name = brand.Name,
        };
        return _mapper.Map<ModelProductResponseModel>(modelToUpdated);
    }
}

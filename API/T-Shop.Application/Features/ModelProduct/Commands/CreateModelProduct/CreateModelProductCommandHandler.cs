﻿using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Commands.CreateModelProduct;
public class CreateModelProductCommandHandler : IRequestHandler<CreateModelProductCommand.CreateModelProductCommand, ModelProductResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Model> _modelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IModelQueries _modelQueries;
    private readonly IBrandQueries _brandQueries;

    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public CreateModelProductCommandHandler(IMapper mapper, IGenericRepository<Model> modelRepository, IUnitOfWork unitOfWork, IModelQueries modelQueries, IBrandQueries brandQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _modelRepository = unitOfWork.GetBaseRepo<Model>();
        _unitOfWork = unitOfWork;
        _modelQueries = modelQueries;
        _brandQueries = brandQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<ModelProductResponseModel> Handle(CreateModelProductCommand.CreateModelProductCommand request, CancellationToken cancellationToken)
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

        var newModel = _mapper.Map<Model>(request);
        _modelRepository.Add(newModel);
        await _unitOfWork.CompleteAsync();
        newModel.Brand = new Domain.Entity.Brand()
        {
            Id = brand.Id,
            Name = brand.Name,
        };


        UpdateExistedCache(newModel);

        var result = _mapper.Map<ModelProductResponseModel>(newModel);
        return result;
    }

    private async void UpdateExistedCache(Model newModel)
    {
        var key = _cacheKeyConstants.ModelCacheKey;
        var brandKey = _cacheKeyConstants.BrandCacheKey;
        var cacheModelValues = await _cache.GetAsync<List<Model>>(key);
        if (cacheModelValues != null)
        {
            //Add model to cache model
            cacheModelValues.Add(newModel);
            _cache.Add(key, cacheModelValues);
            //Add model to cache brand
            _cache.Remove(brandKey);
        }
    }
}

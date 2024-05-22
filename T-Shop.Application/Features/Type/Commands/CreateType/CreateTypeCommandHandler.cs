
using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;
namespace T_Shop.Application.Features.Type.Commands.CreateType;
public class CreateTypeCommandHandler : IRequestHandler<CreateTypeCommand, TypeResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITypeQueries _typeQueries;

    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;

    public CreateTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITypeQueries typeQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
        _typeQueries = typeQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<TypeResponseModel> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        if (await _typeQueries.CheckIsTypeExisted(request.Name)) throw new ConflictException("The type is existed");

        var newType = _mapper.Map<TypeProduct>(request);
        var typeAdded = _typeRepository.Add(newType);
        await _unitOfWork.CompleteAsync();

        //update cache
        UpdateExistedCache(newType);

        var result = _mapper.Map<TypeResponseModel>(typeAdded);
        return result;
    }

    private async void UpdateExistedCache(TypeProduct newType)
    {
        var key = _cacheKeyConstants.TypeCacheKey;
        var cacheValues = await _cache.GetAsync<List<TypeProduct>>(key);
        if (cacheValues != null)
        {
            cacheValues.Add(newType);
            _cache.Add(key, cacheValues);
        }
    }
}


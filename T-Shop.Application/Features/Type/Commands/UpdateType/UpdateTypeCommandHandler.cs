using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Commands.UpdateType;
public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, TypeResponseModel>
{
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly ITypeQueries _typeQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public UpdateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITypeQueries typeQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
        _typeQueries = typeQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<TypeResponseModel> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeQueries.GetTypeByIdAsync(request.Id);
        if (type == null)
        {
            throw new NotFoundException("Type not found");
        }
        var isTypeExisted = await _typeQueries.CheckIsTypeExisted(request.Name);

        if (isTypeExisted && (type.Name.ToLower() != request.Name.ToLower()))
        {
            throw new ConflictException("Type is existed");
        }
        var typeUpdated = _mapper.Map<TypeProduct>(request);
        _typeRepository.Update(typeUpdated);
        await _unitOfWork.CompleteAsync();
        _unitOfWork.Detach(typeUpdated);
        //Update cache
        UpdateExistedCache(typeUpdated);

        return _mapper.Map<TypeResponseModel>(typeUpdated);
    }

    private async void UpdateExistedCache(TypeProduct newType)
    {
        var key = _cacheKeyConstants.TypeCacheKey;
        var cacheValues = await _cache.GetAsync<List<TypeProduct>>(key);
        if (cacheValues != null)
        {
            var typeToUpdate = cacheValues.RemoveAll(t => t.Id.Equals(newType.Id));
            cacheValues.Add(newType);
            _cache.Add(key, cacheValues);
        }
    }
}

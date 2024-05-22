using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Command.CreateBrand;
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entity.Brand> _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBrandQueries _brandQueries;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;

    public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IBrandQueries brandQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _brandRepository = unitOfWork.GetBaseRepo<Domain.Entity.Brand>();
        _unitOfWork = unitOfWork;
        _brandQueries = brandQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<BrandResponseModel> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        if (await _brandQueries.CheckIsBrandExisted(request.Name)) throw new ConflictException("The Brand is existed");

        var newBrand = _mapper.Map<Domain.Entity.Brand>(request);
        _brandRepository.Add(newBrand);
        await _unitOfWork.CompleteAsync();

        //update cache
        UpdateExistedCache(newBrand);

        var result = _mapper.Map<BrandResponseModel>(newBrand);
        return result;
    }
    private async void UpdateExistedCache(Domain.Entity.Brand newBrand)
    {
        var key = _cacheKeyConstants.BrandCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Brand>>(key);
        if (cacheValues != null)
        {
            cacheValues.Add(newBrand);
            _cache.Add(key, cacheValues);
        }
    }

}

using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.CreateColor;
public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, ColorResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IColorQueries _colorQueries;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public CreateColorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IColorQueries colorQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _mapper = mapper;
        _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _unitOfWork = unitOfWork;
        _colorQueries = colorQueries;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<ColorResponseModel> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        if (await _colorQueries.CheckIsColorExisted(request.Name)) throw new ConflictException("The color is existed");

        var newType = _mapper.Map<Domain.Entity.Color>(request);
        var colorAdded = _colorRepository.Add(newType);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(colorAdded);

        var result = _mapper.Map<ColorResponseModel>(colorAdded);
        return result;
    }

    private async void UpdateExistedCache(Domain.Entity.Color colorAdded)
    {
        var key = _cacheKeyConstants.ColorCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Color>>(key);
        if (cacheValues != null)
        {
            cacheValues.Add(colorAdded);
            _cache.Add(key, cacheValues);
        }
    }
}

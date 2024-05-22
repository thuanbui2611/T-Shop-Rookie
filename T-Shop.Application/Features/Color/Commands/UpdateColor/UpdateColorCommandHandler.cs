using AutoMapper;
using LazyCache;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.UpdateColor;
public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, ColorResponseModel>
{
    private readonly IGenericRepository<Domain.Entity.Color> _colorRepository;
    private readonly IColorQueries _colorQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _cache;
    private CacheKeyConstants _cacheKeyConstants;
    public UpdateColorCommandHandler(IColorQueries colorQueries, IUnitOfWork unitOfWork, IMapper mapper, IAppCache cache, CacheKeyConstants cacheKeyConstants)
    {
        _colorRepository = unitOfWork.GetBaseRepo<Domain.Entity.Color>();
        _colorQueries = colorQueries;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
        _cacheKeyConstants = cacheKeyConstants;
    }

    public async Task<ColorResponseModel> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorQueries.GetColorByIdAsync(request.ID);
        if (color == null)
        {
            throw new NotFoundException("Color not found");
        }
        var isColorExisted = await _colorQueries.CheckIsColorExisted(request.Name);

        if (isColorExisted && (color.Name.ToLower() != request.Name.ToLower()))
        {
            throw new ConflictException("Color is existed");
        }
        var colorUpdated = _mapper.Map<Domain.Entity.Color>(request);
        _colorRepository.Update(colorUpdated);
        await _unitOfWork.CompleteAsync();

        UpdateExistedCache(colorUpdated);
        return _mapper.Map<ColorResponseModel>(colorUpdated);
    }

    private async void UpdateExistedCache(Domain.Entity.Color colorUpdated)
    {
        var key = _cacheKeyConstants.ColorCacheKey;
        var cacheValues = await _cache.GetAsync<List<Domain.Entity.Color>>(key);
        if (cacheValues != null)
        {
            var typeToUpdate = cacheValues.RemoveAll(t => t.Id.Equals(colorUpdated.Id));
            cacheValues.Add(colorUpdated);
            _cache.Add(key, cacheValues);
        }
    }
}

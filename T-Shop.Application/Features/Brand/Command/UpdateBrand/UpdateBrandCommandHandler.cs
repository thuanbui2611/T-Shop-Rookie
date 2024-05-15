using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Command.UpdateBrand;
public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandResponseModel>
{
    private readonly IGenericRepository<Domain.Entity.Brand> _brandRepository;
    private readonly IBrandQueries _brandQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandQueries brandQueries, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _brandRepository = unitOfWork.GetBaseRepo<Domain.Entity.Brand>();
        _brandQueries = brandQueries;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BrandResponseModel> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandQueries.GetBrandByIdAsync(request.ID);
        if (brand == null)
        {
            throw new NotFoundException("Brand not found");
        }
        var isBrandExisted = await _brandQueries.CheckIsBrandExisted(request.Name);

        if (isBrandExisted && (brand.Name.ToLower() != request.Name.ToLower()))
        {
            throw new ConflictException("Brand is existed");
        }
        var brandUpdated = _mapper.Map<Domain.Entity.Brand>(request);
        _brandRepository.Update(brandUpdated);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<BrandResponseModel>(brandUpdated);
    }
}

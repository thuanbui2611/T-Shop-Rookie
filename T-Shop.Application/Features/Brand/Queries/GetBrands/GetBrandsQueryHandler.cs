using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Queries.GetBrands;
public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IBrandQueries _brandQueries;

    public GetBrandsQueryHandler(IMapper mapper, IBrandQueries brandQueries)
    {
        _mapper = mapper;
        _brandQueries = brandQueries;
    }

    public async Task<List<BrandResponseModel>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandQueries.GetBrandsAsync();
        var result = _mapper.Map<List<BrandResponseModel>>(brands);
        return result;
    }
}

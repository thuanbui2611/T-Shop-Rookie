using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Queries.GetBrandById;
public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IBrandQueries _brandQueries;

    public GetBrandByIdQueryHandler(IMapper mapper, IBrandQueries brandQueries)
    {
        _mapper = mapper;
        _brandQueries = brandQueries;
    }

    public async Task<BrandResponseModel> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandQueries.GetBrandByIdAsync(request.ID);
        if (brand == null)
        {
            throw new NotFoundException("Brand can not found");
        }
        var result = _mapper.Map<BrandResponseModel>(brand);
        return result;
    }
}

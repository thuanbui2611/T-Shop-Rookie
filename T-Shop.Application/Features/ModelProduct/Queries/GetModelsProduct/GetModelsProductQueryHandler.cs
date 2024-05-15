using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
public class GetModelsProductQueryHandler : IRequestHandler<GetModelsProductQuery, List<ModelProductResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IModelQueries _modelQueries;

    public GetModelsProductQueryHandler(IMapper mapper, IModelQueries modelQueries)
    {
        _mapper = mapper;
        _modelQueries = modelQueries;
    }

    public async Task<List<ModelProductResponseModel>> Handle(GetModelsProductQuery request, CancellationToken cancellationToken)
    {
        var models = await _modelQueries.GetModelsAsync();
        var result = _mapper.Map<List<ModelProductResponseModel>>(models);
        return result;
    }
}

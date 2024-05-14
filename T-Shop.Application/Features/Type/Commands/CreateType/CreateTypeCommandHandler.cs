
using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;
namespace T_Shop.Application.Features.Type.Commands.CreateType;
public class CreateTypeCommandHandler : IRequestHandler<CreateTypeCommand, TypeResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITypeQueries _typeQueries;

    public CreateTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITypeQueries typeQueries)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
        _typeQueries = typeQueries;
    }

    public async Task<TypeResponseModel> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        if (await _typeQueries.CheckIsTypeExisted(request.Name)) throw new ConflictException("The type is existed");

        var newType = _mapper.Map<TypeProduct>(request);
        _typeRepository.Add(newType);
        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<TypeResponseModel>(newType);
        return result;
    }
}


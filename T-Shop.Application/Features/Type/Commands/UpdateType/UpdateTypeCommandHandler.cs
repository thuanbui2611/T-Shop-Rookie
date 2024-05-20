using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Commands.UpdateType;
public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, TypeResponseModel>
{
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly ITypeQueries _typeQueries;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITypeQueries typeQueries)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
        _typeQueries = typeQueries;
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

        return _mapper.Map<TypeResponseModel>(typeUpdated);
    }
}

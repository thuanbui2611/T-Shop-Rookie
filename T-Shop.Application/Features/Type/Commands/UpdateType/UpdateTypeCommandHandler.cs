using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Commands.UpdateType;
public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, TypeResponseModel>
{
    private readonly IGenericRepository<TypeProduct> _typeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _typeRepository = unitOfWork.GetBaseRepo<TypeProduct>();
    }

    public async Task<TypeResponseModel> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetById(request.Id);
        if (type == null)
        {
            throw new NotFoundException("Type not found");
        }
        var typeUpdated = _mapper.Map<TypeProduct>(type);
        _typeRepository.Update(typeUpdated);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<TypeResponseModel>(typeUpdated);
    }
}

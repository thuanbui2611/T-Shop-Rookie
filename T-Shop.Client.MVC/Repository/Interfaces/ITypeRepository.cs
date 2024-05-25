using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface ITypeRepository
    {
        Task<List<TypeResponseModel>> GetTypesAsync();
    }
}

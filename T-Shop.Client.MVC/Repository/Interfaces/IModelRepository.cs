using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IModelRepository
    {
        Task<List<ModelProductResponseModel>> GetModelsAsync();
    }
}

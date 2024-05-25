using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IColorService
    {
        Task<ColorResponseModel> GetColorsAsync();
    }
}

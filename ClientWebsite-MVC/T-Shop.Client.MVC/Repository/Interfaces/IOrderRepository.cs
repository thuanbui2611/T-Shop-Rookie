using T_Shop.Shared.DTOs.Order.RequestModel;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Interfaces;

public interface IOrderRepository
{
    Task<OrderResponseModel> CreateOrUpdateOrderAsync(OrderRequestModel order);
    OrderResponseModel GetOrderOfUser();
    void CleanOrder();
}

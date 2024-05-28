﻿using Newtonsoft.Json;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Services.Services;
using T_Shop.Shared.DTOs.Order.RequestModel;
using T_Shop.Shared.DTOs.Order.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Repository;

public class OrderRepository : BaseRepository, IOrderRepository
{
    private static OrderResponseModel? _orderOfUser = null;

    public OrderRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
    {

    }

    public OrderResponseModel GetOrderOfUser()
    {
        return _orderOfUser;
    }

    public async Task<OrderResponseModel> CreateOrUpdateOrderAsync(OrderRequestModel order)
    {
        var requestUrl = "api/order";
        HttpResponseMessage response = _httpClient.PostAsJsonAsync(requestUrl, order).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            _orderOfUser = JsonConvert.DeserializeObject<OrderResponseModel>(data);
            return _orderOfUser;
        }
        return _orderOfUser;
    }

    public bool RemoveOrder()
    {
        _orderOfUser = null;
        return true;
    }
}
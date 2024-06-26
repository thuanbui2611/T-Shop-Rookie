﻿using Newtonsoft.Json;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Repository
{
    public class CartRepository : ICartRepository
    {
        private static CartResponseModel? _cart = null;
        private readonly HttpClient _httpClient;
        public CartRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartResponseModel> GetCurrentCart(Guid userId)
        {
            if (_cart == null)
            {
                _cart = await GetCartByUserIdAsync(userId);
            }
            return _cart;
        }

        public async Task<CartResponseModel> GetCartByUserIdAsync(Guid userId)
        {
            var requestUrl = "api/cart/" + userId;
            HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                _cart = JsonConvert.DeserializeObject<CartResponseModel>(data);
                return _cart;
            }

            return _cart;
        }

        public async Task<CartResponseModel> AddToCartAsync(CartRequestModel newItem)
        {
            var requestUrl = "api/cart";
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(requestUrl, newItem).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                _cart = JsonConvert.DeserializeObject<CartResponseModel>(data);
                return _cart;
            }
            return _cart;
        }

        public async Task<CartResponseModel> UpdateCartItemAsync(CartRequestModel cartRequestModel)
        {
            var requestUrl = $"api/cart/{cartRequestModel.UserID}";
            HttpResponseMessage response = _httpClient.PutAsJsonAsync(requestUrl, cartRequestModel).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                _cart = JsonConvert.DeserializeObject<CartResponseModel>(data);
                return _cart;
            }
            return _cart;
        }

        public async Task<bool> DeleteCartItemAsync(CartItemDeleteRequestModel cartItemRequestModel)
        {
            var requestUrl = $"api/cart/{cartItemRequestModel.UserID}/{cartItemRequestModel.ProductID}";
            HttpResponseMessage response = _httpClient.DeleteAsync(requestUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                _cart.CartItems.RemoveAll(c => c.Product.Id.Equals(cartItemRequestModel.ProductID));
                return true;
            }
            return false;
        }

        public void ClearCart()
        {
            _cart = null;
        }
    }
}

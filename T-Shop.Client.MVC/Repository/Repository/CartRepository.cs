using Newtonsoft.Json;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Services.Services;
using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Repository
{
    public class CartRepository : BaseRepository, ICartRepository
    {
        private static CartResponseModel? _cart = null;
        public CartRepository(HttpClient httpClient) : base(httpClient)
        {

        }

        public CartResponseModel GetCart()
        {
            return _cart;
        }

        public async Task<CartResponseModel?> AddToCartAsync(CartRequestModel newItem)
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

        public async Task<CartResponseModel?> GetCartByUserIdAsync(Guid userId)
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
    }
}

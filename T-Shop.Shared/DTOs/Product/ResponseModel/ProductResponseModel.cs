using T_Shop.Shared.DTOs.Color.ResponseModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Shared.DTOs.Product.ResponseModel
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Variant { get; set; }
        public int Quantity { get; set; }
        public decimal? Rating { get; set; }
        public int? totalReviews { get; set; }
        public bool IsOnStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public TypeResponseModel Type { get; set; }
        public ModelProductResponseModel Model { get; set; }
        public ColorResponseModel Color { get; set; }
        public List<ImageOfProductResponseModel> Images { get; set; } = new();

    }

    public class ImageOfProductResponseModel
    {
        public Guid imageID { get; set; }
        public string imageUrl { get; set; }
        public bool isMain { get; set; }
    }
}

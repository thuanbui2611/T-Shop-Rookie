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
        public string CreatedAt { get; set; }
        public string IsOnStock { get; set; }
        public string Variant { get; set; }
        public decimal? Rating { get; set; }
        public int? totalReviews { get; set; }
        public TypeResponseModel Type { get; set; }
        public ModelProductResponseModel Model { get; set; }
        public ColorResponseModel Color { get; set; }
        public List<ImageOfProductResponseModel> Images { get; set; } = new();

    }

    public class ImageOfProductResponseModel
    {
        public Guid ImageID { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}

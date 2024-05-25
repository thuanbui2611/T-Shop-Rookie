using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Shared.ViewModels.ProductsPage
{
    public class ProductVM
    {
        public List<ProductResponseModel> Products { get; set; }
        public List<BrandResponseModel> Brands { get; set; }
        public List<ModelProductResponseModel> Models { get; set; }
        public List<TypeResponseModel> Types { get; set; }
        public List<ColorResponseModel> Colors { get; set; }
    }
}

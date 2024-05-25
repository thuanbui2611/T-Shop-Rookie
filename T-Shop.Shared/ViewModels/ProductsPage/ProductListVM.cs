using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Shared.ViewModels.ProductsPage
{
    public class ProductListVM
    {
        public List<ProductResponseModel> Products { get; set; } = [];
        public MetaData PaginationMetaData { get; set; }
    }

    public class MetaData
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}

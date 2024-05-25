using T_Shop.Shared.DTOs.Product.QueryModel;

namespace T_Shop.Shared.ViewModels.ProductsPage
{
    public class ProductRequestParam : ProductQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

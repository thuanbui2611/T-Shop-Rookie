namespace T_Shop.Shared.ViewModels.ProductsPage
{
    public class ProductRequestParam
    {
        public string? Types { get; set; }
        public string? Brands { get; set; }
        public string? Models { get; set; }
        public string? Colors { get; set; }
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

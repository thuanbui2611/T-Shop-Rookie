using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Shared.ViewModels.ProductsPage
{
    public class ProductReviewListVM
    {
        public List<ProductReviewResponseModel> ProductReviews { get; set; } = [];
        public MetaData PaginationMetaData { get; set; }
    }
}

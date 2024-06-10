using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetNumberReviewsOfProduct;
public class GetNumberReviewsOfProductQueryHandler : IRequestHandler<GetNumberReviewsOfProductQuery, ProductReviewCountResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IProductReviewQueries _productReviewQueries;

    public GetNumberReviewsOfProductQueryHandler(IMapper mapper, IProductReviewQueries productReviewQueries)
    {
        _mapper = mapper;
        _productReviewQueries = productReviewQueries;
    }

    public async Task<ProductReviewCountResponseModel> Handle(GetNumberReviewsOfProductQuery request, CancellationToken cancellationToken)
    {
        var numberOfReviews = await _productReviewQueries.CountTotalReviewByProductId(request.ProductID);
        var result = new ProductReviewCountResponseModel()
        {
            ProductID = request.ProductID,
            TotalReviews = numberOfReviews
        };
        return result;
    }
}

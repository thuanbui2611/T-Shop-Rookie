using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetReviewsOfProduct;
public class GetReviewsOfProductQueryHandler : IRequestHandler<GetReviewsOfProductQuery, (List<ProductReviewResponseModel>, PaginationMetaData)>
{
    private readonly IMapper _mapper;
    private readonly IProductReviewQueries _productReviewQueries;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetReviewsOfProductQueryHandler(IMapper mapper, IProductReviewQueries productReviewQueries, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _productReviewQueries = productReviewQueries;
        _userManager = userManager;
    }

    public async Task<(List<ProductReviewResponseModel>, PaginationMetaData)> Handle(GetReviewsOfProductQuery request, CancellationToken cancellationToken)
    {
        var (reviews, pagination) = await _productReviewQueries.GetProductReviewsByProductIdAsync(request.ProductID, request.Pagination);

        var userIds = reviews.Select(pr => pr.UserID).Distinct().ToList();
        var users = await _userManager.Users
           .Where(u => userIds.Contains(u.Id))
           .ToListAsync();
        var userResponseModels = _mapper.Map<List<UserOfReview>>(users);

        var result = reviews.Select(review =>
        {
            var reviewDto = _mapper.Map<ProductReviewResponseModel>(review);
            reviewDto.User = userResponseModels.FirstOrDefault(u => u.ID.Equals(review.UserID));
            return reviewDto;

        }).ToList();
        return (result, pagination);
    }
}

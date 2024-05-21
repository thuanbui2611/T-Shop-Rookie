﻿using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetReviewsOfProduct;
public class GetReviewsOfProductQueryHandler : IRequestHandler<GetReviewsOfProductQuery, List<ProductReviewResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IProductReviewQueries _productReviewQueries;

    public GetReviewsOfProductQueryHandler(IMapper mapper, IProductReviewQueries productReviewQueries)
    {
        _mapper = mapper;
        _productReviewQueries = productReviewQueries;
    }

    public async Task<List<ProductReviewResponseModel>> Handle(GetReviewsOfProductQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _productReviewQueries.GetAllProductReviewByProductIdAsync(request.ProductID);
        var result = _mapper.Map<List<ProductReviewResponseModel>>(reviews);
        return result;
    }
}
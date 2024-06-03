using AutoMapper;
using MediatR;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Commands.CreateReviewForProduct;
public class CreateReviewForProductCommandHandler : IRequestHandler<CreateReviewForProductCommand, ProductReviewResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    private readonly IUserQueries _userQueries;
    private readonly ITransactionQueries _transactionQueries;
    private readonly IGenericRepository<ProductReviewImage> _reviewImageRepository;

    private readonly IGenericRepository<Domain.Entity.ProductReview> _productReviewRepository;
    public CreateReviewForProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService, IUserQueries userQueries, ITransactionQueries transactionQueries)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _userQueries = userQueries;
        _transactionQueries = transactionQueries;

        _productReviewRepository = unitOfWork.GetBaseRepo<Domain.Entity.ProductReview>();
        _reviewImageRepository = unitOfWork.GetBaseRepo<ProductReviewImage>();
    }

    public async Task<ProductReviewResponseModel> Handle(CreateReviewForProductCommand request, CancellationToken cancellationToken)
    {
        await HandleValidatingReview(request);

        var productReview = new Domain.Entity.ProductReview()
        {
            UserID = request.UserID,
            ProductID = request.ProductID,
            Title = request.Title,
            Content = request.Content,
            Rating = request.Rating,
            OrderID = request.OrderID,

        };
        _productReviewRepository.Add(productReview);
        //Add images
        List<ProductReviewImage> reviewImages = new List<ProductReviewImage>();
        if (request.ImagesUpload.Count() > 0)
        {
            var images = await _cloudinaryService.AddImagesAsync(request.ImagesUpload);
            //Add images to table product review image

            foreach (var image in images)
            {
                ProductReviewImage productImage = new ProductReviewImage()
                {
                    ProductReviewID = productReview.Id,
                    ImagePublicID = image.PublicID
                };
                reviewImages.Add(productImage);
            }
            _reviewImageRepository.AddRange(reviewImages);
        }
        await _unitOfWork.CompleteAsync();
        productReview.ProductReviewImages = reviewImages;
        var result = _mapper.Map<ProductReviewResponseModel>(productReview);
        return result;
    }

    private async Task HandleValidatingReview(CreateReviewForProductCommand request)
    {
        if (request.ProductID == Guid.Empty || request.OrderID == Guid.Empty || request.UserID == Guid.Empty || request.TransactionID == Guid.Empty)
        {
            throw new NotFoundException("The ID is empty");
        }
        if (!TransactionConstants.ALLOW_RATING_INPUT.Contains(request.Rating))
        {
            throw new BadRequestException($"Rating does not allow, only allow: " +
                $"{String.Join(", ", TransactionConstants.ALLOW_RATING_INPUT)}");
        }

        var isUserExisted = await _userQueries.CheckIfUserExisted(request.UserID);
        if (!isUserExisted)
        {
            throw new NotFoundException("User not found");
        }

        var transaction = await _transactionQueries.GetTransactionByIdAsync(request.TransactionID, false);
        if (transaction == null)
        {
            throw new NotFoundException("Transaction not found");
        }
        if (transaction.Status != TransactionConstants.COMPLETED)
        {
            throw new BadRequestException("Transaction is not completed to create a review");
        }
        //Check product is existed in order
        if (!transaction.Order.OrderDetails.Any(x => x.ProductID.Equals(request.ProductID)))
        {
            throw new NotFoundException("Product is not found in transaction");
        }

    }
}

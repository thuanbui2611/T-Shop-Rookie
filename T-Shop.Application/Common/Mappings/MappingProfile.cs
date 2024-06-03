using AutoMapper;
using T_Shop.Application.Common.Constants;
using T_Shop.Application.Features.Brand.Command.CreateBrand;
using T_Shop.Application.Features.Brand.Command.UpdateBrand;
using T_Shop.Application.Features.Products.Commands.CreateProduct;
using T_Shop.Application.Features.Products.Commands.UpdateProduct;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Domain.Entity;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;
using T_Shop.Shared.DTOs.Color.RequestModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;
using T_Shop.Shared.DTOs.ModelProduct.RequestModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Order.ResponseModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;
using static T_Shop.Shared.DTOs.ModelProduct.ResponseModel.ModelProductResponseModel;

namespace T_Shop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<ProductImage, ImageOfProductResponseModel>()
                .ForMember(dest => dest.imageUrl, src => src.MapFrom(src => string.Concat(ImageConstants.PathUrl, src.ImagePublicID)))
                .ForMember(dest => dest.isMain, src => src.MapFrom(src => src.IsMain));
            CreateMap<Product, ProductResponseModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .AfterMap((src, dest) =>
                {
                    dest.Name = $"{src.Model.Brand.Name} {src.Model.Name} {src.Variant}";
                    if (src.ProductReviews != null)
                    {
                        dest.Rating = src.ProductReviews.Any() ? (decimal)src.ProductReviews.Average(x => x.Rating) : 0;
                        dest.totalReviews = src.ProductReviews.Any() ? src.ProductReviews.Count() : 0;
                    }

                });
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            //Type
            CreateMap<TypeProduct, TypeResponseModel>();
            CreateMap<CreateTypeCommand, TypeProduct>();
            CreateMap<UpdateTypeCommand, TypeProduct>();

            //Brand
            CreateMap<Model, ModelOfBrand>();
            CreateMap<Brand, BrandResponseModel>()
                .ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<UpdateBrandCommand, Brand>();

            //Model
            CreateMap<Model, ModelProductResponseModel>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => new BrandOfModel
                {
                    ID = src.Brand.Id,
                    Name = src.Brand.Name,
                }));
            CreateMap<ModelCreationRequestModel, Model>();
            CreateMap<ModelUpdateRequestModel, Model>();

            //Color
            CreateMap<Color, ColorResponseModel>();
            CreateMap<ColorCreationRequestModel, Color>();
            CreateMap<ColorUpdateRequestModel, Color>();

            //Cart
            CreateMap<Cart, CartResponseModel>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
            CreateMap<CartItem, CartItemResponseModel>();

            //Order
            CreateMap<Product, ProductOfOrderResponseModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .ForMember(dest => dest.IsReviewed, opt => opt.MapFrom(src => src.OrderDetails.Any(od => od.ProductReview != null)))
                .AfterMap((src, dest) =>
                {
                    dest.Name = $"{src.Model.Brand.Name} {src.Model.Name} {src.Variant}";
                    if (src.ProductReviews != null)
                    {
                        dest.Rating = src.ProductReviews.Any() ? (decimal)src.ProductReviews.Average(x => x.Rating) : 0;
                        dest.totalReviews = src.ProductReviews.Any() ? src.ProductReviews.Count() : 0;
                    }
                });

            CreateMap<OrderDetail, OrderDetailResponseModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Order, OrderResponseModel>()
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            //Transaction
            CreateMap<Transaction, TransactionResponseModel>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));
            CreateMap<Transaction, TransactionWithCustomerResponseModel>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            //ProductReview
            CreateMap<ProductReviewImage, ProductReviewImagesResponseModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => string.Concat(ImageConstants.PathUrl, src.ImagePublicID)));
            CreateMap<ProductReview, ProductReviewResponseModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductReviewImages));
            CreateMap<ApplicationUser, UserOfReview>()
                 .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => string.Concat(ImageConstants.PathUrl, src.Avatar))); ;
            //User
            CreateMap<ApplicationUser, UserResponseModel>()
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => string.Concat(ImageConstants.PathUrl, src.Avatar)));
        }

    }
}

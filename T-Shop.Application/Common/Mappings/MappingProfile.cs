using AutoMapper;
using T_Shop.Application.Features.Brand.Command.CreateBrand;
using T_Shop.Application.Features.Brand.Command.UpdateBrand;
using T_Shop.Application.Features.Products.Commands.CreateProduct;
using T_Shop.Application.Features.Products.Commands.UpdateProduct;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;
using T_Shop.Shared.DTOs.Color.RequestModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;
using T_Shop.Shared.DTOs.ModelProduct.RequestModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Order.ResponseModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;
using static T_Shop.Shared.DTOs.ModelProduct.ResponseModel.ModelProductResponseModel;

namespace T_Shop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<ProductImage, ImageOfProductResponseModel>();
            CreateMap<Product, ProductResponseModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .AfterMap((src, dest) =>
                {
                    dest.Name = $"{src.Model.Brand.Name} {src.Model.Name} {src.Variant}";

                });
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            //Type
            CreateMap<TypeProduct, TypeResponseModel>();
            CreateMap<CreateTypeCommand, TypeProduct>();
            CreateMap<UpdateTypeCommand, TypeProduct>();

            //Brand
            CreateMap<Brand, BrandResponseModel>();
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
            CreateMap<OrderDetail, OrderDetailResponseModel>();
            CreateMap<Order, OrderResponseModel>()
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

        }

    }
}

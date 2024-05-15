using AutoMapper;
using T_Shop.Application.Features.Brand.Command.CreateBrand;
using T_Shop.Application.Features.Brand.Command.UpdateBrand;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Color.RequestModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;
using T_Shop.Shared.DTOs.ModelProduct.RequestModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;
using static T_Shop.Shared.DTOs.ModelProduct.ResponseModel.ModelProductResponseModel;

namespace T_Shop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            //CreateMap<Product, ProductDto>()
            //    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfProduct
            //    {
            //        Id = src.Category.Id,
            //        Name = src.Category.Name,

            //    }));

            //CreateMap<CreateProductCommand, Product>();
            //CreateMap<Product, CreateProductCommand>();
            //CreateMap<UpdateProductCommand, Product>();

            ////Category
            //CreateMap<Category, CategoryDto>();
            //CreateMap<CreateCategoryCommand, Category>();
            //CreateMap<UpdateCategoryCommand, Category>();

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
        }
    }
}

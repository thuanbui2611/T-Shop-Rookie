using AutoMapper;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Type.ResponseModel;

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
        }
    }
}

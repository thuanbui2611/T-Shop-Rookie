using AutoMapper;
using T_Shop.Application.Features.Categories.Commands.CreateCategory;
using T_Shop.Application.Features.Categories.Commands.UpdateCategory;
using T_Shop.Application.Features.Products.Commands.CreateProduct;
using T_Shop.Application.Features.Products.Commands.UpdateProduct;
using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Category;
using T_Shop.Shared.DTOs.Product;

namespace T_Shop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryOfProduct
                {
                    Id = src.Category.Id,
                    Name = src.Category.Name,

                }));

            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, CreateProductCommand>();
            CreateMap<UpdateProductCommand, Product>();

            //Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            //User
        }
    }
}

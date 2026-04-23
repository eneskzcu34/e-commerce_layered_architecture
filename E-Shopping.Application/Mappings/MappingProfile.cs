using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Shopping.Application.DTOs.CategoryDTos;
using E_Shopping.Application.DTOs.ProductDTos;
using E_Shopping.Domain.Entities;

namespace E_Shopping.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.MainImageUrl,
                    opt => opt.MapFrom(src =>
                        src.Images.FirstOrDefault(x => x.IsMain).ImageUrl
                    ));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Product, ProductUpdateDto>();

            CreateMap<Product, ProductGetByIdDto>().ReverseMap();
            CreateMap<ProductImages, ProductImagesDto>().ReverseMap();
            /*------------------------------------------------------------*/

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ForMember(dest => dest.ProductCount,
                                                            opt => opt.MapFrom(src => src.Products.Count));
            CreateMap<CategoryUpdateDtos, Category>().ReverseMap();
        }
    }
}
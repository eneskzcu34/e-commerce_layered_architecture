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
            CreateMap<Product, ProductListDto>().ForMember(dest => dest.CategoryName,
                                                            opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Product, ProductGetByIdDto>().ReverseMap();

            /*------------------------------------------------------------*/

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ForMember(dest => dest.ProductCount,
                                                            opt => opt.MapFrom(src => src.Products.Count));

        }
    }
}
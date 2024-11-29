using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Dtos.Response.Review;
using Eshop.Domain.Entities;

namespace Eshop.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
        
            CreateMap<Product,ProductDtoResponse>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.CategoryName));
            CreateMap<ProductDtoCreateRequest,Product>();
            CreateMap<ProductDtoUpdateRequest,Product>();

            CreateMap<Review,ReviewDtoResponse>();
            CreateMap<ReviewDtoCreateRequest,Review>();
            CreateMap<ReviewDtoUpdateRequest,Review>();

            CreateMap<BasketProduct,BasketProductDtoResponse>();
            CreateMap<BasketProductDtoCreateRequest,BasketProduct>();
            CreateMap<Basket, BasketDtoResponse>().ForMember(dest=>dest.BasketProductDtoResponses,opt =>opt.MapFrom(src=>src.BasketProducts));

            CreateMap<Category,CategoryDtoResponse>().ForMember(dest => dest.Name, opt => opt.MapFrom(src=>src.CategoryName));
            CreateMap<CategoryDtoUpdateRequest,Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src=>src.Name));
            CreateMap<CategoryDtoCreateRequest,Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src=>src.Name));




        }
    }
}
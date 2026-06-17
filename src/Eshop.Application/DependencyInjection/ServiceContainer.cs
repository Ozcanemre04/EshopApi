using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Mapper;
using Eshop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Application.DependencyInjection
{
    public static class ServiceContainer
    {
         public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            
            services.AddAutoMapper( typeof(AutoMapperProfile));
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IReviewService,ReviewService>();
            services.AddScoped<IBasketProductService,BasketProductService>();
            
            return services;
        }
    }
}
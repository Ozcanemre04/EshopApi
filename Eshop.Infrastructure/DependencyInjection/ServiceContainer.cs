
using System.Text;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Infrastructure.Data;
using Eshop.Infrastructure.Identity;
using Eshop.Infrastructure.Interface;
using Eshop.Infrastructure.Mapper;
using Eshop.Infrastructure.Repository;
using Eshop.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper( typeof(AutoMapperAuthProfile));
            services.AddHttpContextAccessor();
            // database
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            //identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;

            });
            //jwt
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                    ValidAudience = configuration.GetSection("JWT:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value))

                };

            });
            // service
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ICurrentUserService,CurrentUserService>();
            services.AddScoped<ITokenService,TokenService>();
            //repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketProductRepository, BasketProductRepository>();
            return services;
        }
    }
}
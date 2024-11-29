using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;
using Eshop.Domain.Entities.commun;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eshop.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _config;
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration config) : base(options) {
            _config = config;
         }


        public virtual required DbSet<Product> Products { get; set; }
        public virtual required DbSet<Review> Reviews { get; set; }
        public virtual required DbSet<Category> Categories { get; set; }
        public virtual required DbSet<Basket> Baskets { get; set; }
        public virtual required DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "USER",
                NormalizedName = "USER",
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                ConcurrencyStamp = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
            }, new IdentityRole
            {
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                ConcurrencyStamp = "341743f0-asd2–42de-afbf-59kmkkmk72cf6"
            });
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser{
                FirstName = _config.GetSection("Admin:FirstName").Value,
                LastName = _config.GetSection("Admin:LastName").Value,
                UserName = _config.GetSection("Admin:UserName").Value,
                Email = _config.GetSection("Admin:Email").Value,
                Refreshtoken = _config.GetSection("Admin:Refreshtoken").Value,
                RefreshTokenExpireTime = DateTime.UtcNow.AddDays(5),
                PasswordHash = hasher.HashPassword(null,_config.GetSection("Admin:Password").Value),
                NormalizedEmail=_config.GetSection("Admin:Email").Value.ToUpper(),
                NormalizedUserName=_config.GetSection("Admin:UserName").Value.ToUpper(),
                Id =_config.GetSection("Admin:Id").Value

            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>{
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = _config.GetSection("Admin:Id").Value
            });

            modelBuilder.Entity<Category>().HasData(
                new Category {
                    Id = 1,
                    CategoryName = "Men's clothing"
                },
                new Category {
                    Id = 2,
                    CategoryName = "Women's clothing"
                },
                new Category {
                    Id = 3,
                    CategoryName = "jewelery"
                },
                new Category {
                    Id = 4,
                    CategoryName = "electronics"
                }
                
            );

            modelBuilder.Entity<Category>()
                        .HasMany<Product>(p => p.Products)
                        .WithOne(p => p.category);

            modelBuilder.Entity<Product>()
                        .HasMany<Review>(p => p.Reviews)
                        .WithOne(p => p.Product)
                        .HasForeignKey(K => K.ProductId);

            modelBuilder.Entity<Product>()
                        .HasMany<BasketProduct>(p => p.BasketProducts)
                        .WithOne(p => p.Product)
                        .HasForeignKey(K => K.ProductId);

            modelBuilder.Entity<Basket>()
                        .HasMany<BasketProduct>(p => p.BasketProducts)
                        .WithOne(p => p.Basket)
                        .HasForeignKey(K => K.BasketId);
            // user-> review         
            modelBuilder.Entity<ApplicationUser>()
                        .HasMany<Review>(r => r.Reviews)
                        .WithOne()
                        .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Review>()
                        .HasOne<ApplicationUser>()
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(u => u.UserId);
            // user-> Basket
            modelBuilder.Entity<ApplicationUser>()
                        .HasOne<Basket>(r => r.Basket)
                        .WithOne()
                        .HasForeignKey<Basket>(u => u.UserId);

            modelBuilder.Entity<Basket>()
                        .HasOne<ApplicationUser>()
                        .WithOne(u => u.Basket)
                        .HasForeignKey<Basket>(u => u.UserId);
        }
    }
}
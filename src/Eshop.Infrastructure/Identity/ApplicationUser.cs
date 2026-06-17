
using Microsoft.AspNetCore.Identity;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Refreshtoken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpireTime { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Basket? Basket { get; set; }

    }
}
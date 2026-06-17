using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Infrastructure.Identity;

namespace Eshop.Infrastructure.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);

        string GenerateRefreshToken();
    }
}
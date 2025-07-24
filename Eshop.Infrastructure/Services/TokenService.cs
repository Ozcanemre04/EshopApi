using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Eshop.Infrastructure.Identity;
using Eshop.Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Infrastructure.Services
{
    public class TokenService: ITokenService
    {
         private readonly IConfiguration _config;
          private readonly UserManager<ApplicationUser> _userManager;
    
        public TokenService(IConfiguration config,UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public string GenerateRefreshToken()
        {
            var randonNumber = new Byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randonNumber);
            }
            return Convert.ToBase64String(randonNumber);
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email,user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName ?? ""),
            };

            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string key = _config.GetSection("JWT:Key").Value ?? "";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                issuer: _config.GetSection("JWT:Issuer").Value,
                audience: _config.GetSection("JWT:Audience").Value,
                signingCredentials: signingCred);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eshop.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Eshop.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string UserId => _httpContextAccessor.HttpContext!.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated ? true : false;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;
using Eshop.Infrastructure.Identity;

namespace Eshop.Infrastructure.Mapper
{
    public class AutoMapperAuthProfile : Profile
    {
        public AutoMapperAuthProfile()
        {
            CreateMap<RegisterDtoRequest,ApplicationUser>();
            CreateMap<ApplicationUser,RegisterDtoResponse>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;

namespace Eshop.Application.Test.Fixture
{
    public class PageDtoFixture
    {
        public static PageDto<ProductDtoResponse> pageDtoFixture(){
            return new PageDto<ProductDtoResponse>{
                PageNumber=1,
                PageSize=10,
                TotalPages=ProductDtoFixture.getAllProducts().Count()/ 10,
                TotalRecords=ProductDtoFixture.getAllProducts().Count(),
                Data = ProductDtoFixture.getAllProducts(),
            };
        }
    }
}
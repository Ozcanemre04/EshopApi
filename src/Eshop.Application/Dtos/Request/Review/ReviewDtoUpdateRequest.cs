using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Request.Review
{
    public class ReviewDtoUpdateRequest
    {
        public string? Reviews { get; set; }
        public int? Stars { get; set; }
       
    }
}
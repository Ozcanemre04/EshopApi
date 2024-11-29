using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Commun;

namespace Eshop.Application.Dtos.Response.Review
{
    public class ReviewDtoResponse : BaseDto
    {
        public required string Reviews { get; set; }
        public required int Stars { get; set; }
        public required long ProductId { get; set; }
        public required string UserId { get; set; }
    }
}
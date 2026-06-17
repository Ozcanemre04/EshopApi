using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Commun;

namespace Eshop.Application.Dtos.Response.Category
{
    public class CategoryDtoResponse : BaseDto
    {
        public required string Name { get; set; }
    }
}
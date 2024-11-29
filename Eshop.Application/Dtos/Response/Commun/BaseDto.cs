using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.Commun
{
    public class BaseDto
    {
        public required long Id { get; set; }
        public required DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
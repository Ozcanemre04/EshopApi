using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.Product
{
    public class AverageRatingsDtoResponse
    {
       public long Id { get; set; }
       public int? Sum { get; set; }
       public int? Count { get; set; }
       public double? Average => Count > 0 ? (double)Sum / Count : 0.0D;
    }
}
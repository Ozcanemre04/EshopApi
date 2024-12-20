using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.Commun
{
    public class PageDto<T>
    {
       public int PageNumber { get; set; }
       public int PageSize { get; set; }
       public int TotalPages { get; set; }
       public int TotalRecords { get; set; }
       public IEnumerable<T>? Data { get; set; }
    }
}
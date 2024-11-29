using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities.commun;

namespace Eshop.Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string CategoryName { get; set; }

        public ICollection<Product>? Products{ get; set; }
    }
}
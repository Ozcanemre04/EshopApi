using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities.commun;

namespace Eshop.Domain.Entities
{
    public class BasketProduct : BaseEntity
    {
        public required int Quantity { get; set; } = 1;
        public required  decimal TotalPrice{get;set;}
        [ForeignKey("Product")]
        public required long ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Basket")]
        public required long BasketId { get; set; }
        public Basket? Basket { get; set; }

        
    }
}
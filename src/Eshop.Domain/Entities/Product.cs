using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities.commun;

namespace Eshop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Image { get; set; }
        [ForeignKey("Category")]
        public required long CategoryId { get; set; }
        public Category? category;
        public ICollection<BasketProduct>? BasketProducts { get; set; }
        public ICollection<Review>? Reviews{ get; set; }
    }
}
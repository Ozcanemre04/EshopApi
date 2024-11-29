using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities.commun;

namespace Eshop.Domain.Entities
{
    public class Basket : BaseEntity
    {

        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }

        public ICollection<BasketProduct>? BasketProducts { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities.commun;

namespace Eshop.Domain.Entities
{
    public class Review : BaseEntity
    {
        public required string Reviews { get; set; }
        public required int Stars { get; set; }
        
        [ForeignKey("Product")]
        public required long ProductId { get; set; }
        public required Product Product{ get; set; }

        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        
    }
}
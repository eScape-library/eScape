using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Entities
{
    public class ProductDetails
    {
        public int ProductDetailsId { get; set; }
        public int ProductId { get; set; }
        public int SizeAtributeId { get; set; }
        public int ColorAttributeId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public string SubImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductDetailsId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Price { get; set; }
        public string? ProductImage { get; set; }
    }

}

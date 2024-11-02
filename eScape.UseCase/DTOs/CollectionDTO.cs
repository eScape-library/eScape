using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.DTOs
{
    public class CollectionDTO
    {
        public int ProductDetailsId { get; set; }
        public int ProductId { get; set; }
        public string? Price { get; set; }
        public int Quantity { get; set; }
        public string? ProductImage { get; set; }
        public string? SubImage { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? AvailableColors { get; set; }
    }

    public class CollectionFilterDTO
    {
        public IEnumerable<string>? Color { get; set; }
        public IEnumerable<string>? Size { get; set; }
        public string? Price { get; set; }
    }
}

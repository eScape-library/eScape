using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase
{
    public class ProductDetailsDTO
    {
        public int ProductDetailsId { get; set; }
        public int ProductId { get; set; }
        public int SizeAtributeId { get; set; }
        public int ColorAttributeId { get; set; }
        public string? Price { get; set; }
        public int Quantity { get; set; }
        public string? ProductImage { get; set; }
        public string? SubImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? AvailableColors { get; set; }
        public string? AvailableSizes { get; set; }
    }

    public class ProductDetailsWithFamiliar
    {
        public ProductDetailsDTO? Product {  get; set; }
        public IEnumerable<Variant>? Variants { get; set; }
        public IEnumerable<CollectionDTO>? Familiar { get; set; }
    }

    public class Variant
    {
        public ProductDetailsDTO? VariantColor { get; set; }
        public IEnumerable<ProductDetailsDTO>? VariantSize { get; set; }
    }

}

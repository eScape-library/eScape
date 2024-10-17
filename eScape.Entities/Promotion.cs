using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsActive { get; set; }
        public int ProductDetailsId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public int ConsigneeId { get; set; }
        public string Title { get; set; }
        public string AddressLineMain { get; set; }
        public string AddressLineSup { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Landmark { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

    }

}

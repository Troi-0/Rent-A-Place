using System;
using System.Collections.Generic;

#nullable disable

namespace WebScraper.Models
{
    public partial class RealEstateBooking
    {
        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public string RenterId { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual RealEstate RealEstate { get; set; }
        public virtual AspNetUser Renter { get; set; }
    }
}

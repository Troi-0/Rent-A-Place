using System;
using System.Collections.Generic;

#nullable disable

namespace WebScraper.Models
{
    public partial class RealEstate
    {
        public RealEstate()
        {
            RealEstateBookings = new HashSet<RealEstateBooking>();
            RealEstateTags = new HashSet<RealEstateTag>();
        }

        public int Id { get; set; }
        public int Size { get; set; }
        public int? Floor { get; set; }
        public int? TotalNumberOfFloors { get; set; }
        public int? Year { get; set; }
        public int Rent { get; set; }
        public string OwnerId { get; set; }
        public int DistrictId { get; set; }
        public int RealEstateTypeId { get; set; }
        public int BuildingTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual BuildingType BuildingType { get; set; }
        public virtual District District { get; set; }
        public virtual AspNetUser Owner { get; set; }
        public virtual RealEstateType RealEstateType { get; set; }
        public virtual ICollection<RealEstateBooking> RealEstateBookings { get; set; }
        public virtual ICollection<RealEstateTag> RealEstateTags { get; set; }
    }
}

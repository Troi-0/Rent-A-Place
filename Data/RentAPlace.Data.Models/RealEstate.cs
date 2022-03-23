namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class RealEstate : BaseDeletableModel<int>
    {
        public RealEstate()
        {
            this.Tags = new HashSet<RealEstateTag>();
            this.Bookings = new HashSet<RealEstateBooking>();
        }

        public int Size { get; set; }

        public int? Floor { get; set; }

        public int? TotalNumberOfFloors { get; set; }

        public int? Year { get; set; }

        public int Rent { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public int RealEstateTypeId { get; set; }

        public virtual RealEstateType RealEstateType { get; set; }

        public int BuildingTypeId { get; set; }

        public virtual BuildingType BuildingType { get; set; }

        public ICollection<RealEstateTag> Tags { get; set; }

        public ICollection<RealEstateBooking> Bookings { get; set; }
    }
}

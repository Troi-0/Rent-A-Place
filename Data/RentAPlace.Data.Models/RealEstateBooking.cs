namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class RealEstateBooking : BaseDeletableModel<int>
    {
        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public string RenterId { get; set; }

        public virtual ApplicationUser Renter { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }
    }
}

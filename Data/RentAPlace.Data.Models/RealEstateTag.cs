namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class RealEstateTag : BaseDeletableModel<int>
    {
        public int PropertyId { get; set; }

        public virtual RealEstate Property { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

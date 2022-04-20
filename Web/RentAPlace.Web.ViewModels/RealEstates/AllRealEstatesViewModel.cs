namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllRealEstatesViewModel
    {
        public int RealEstateId { get; set; }

        public string RealEstateTypeName { get; set; }

        public string ImageUrl { get; set; }

        public int Size { get; set; }

        public int Rent { get; set; }

        public string DistrictName { get; set; }
    }
}

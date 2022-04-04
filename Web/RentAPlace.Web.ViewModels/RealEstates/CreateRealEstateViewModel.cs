namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateRealEstateViewModel
    {
        public int Size { get; set; }

        public int Floor { get; set; }

        public int TotalNumberOfFoloors { get; set; }

        public int Year { get; set; }

        public int Rent { get; set; }

        public string DistrictName { get; set; }

        public string RealEstateTypeName { get; set; }

        public string BuildingTypeName { get; set; }
    }
}

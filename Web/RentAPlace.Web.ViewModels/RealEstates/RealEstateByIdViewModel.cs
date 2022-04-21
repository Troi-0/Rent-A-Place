namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System.Collections.Generic;

    public class RealEstateByIdViewModel
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public int? Floor { get; set; }

        public int? TotalNumberOfFloors { get; set; }

        public int Year { get; set; }

        public int Rent { get; set; }

        public string DistrictName { get; set; }

        public string RealEstateTypeName { get; set; }

        public string BuildingTypeName { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }
    }
}

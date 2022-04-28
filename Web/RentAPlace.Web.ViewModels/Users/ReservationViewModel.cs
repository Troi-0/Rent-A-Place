namespace RentAPlace.Web.ViewModels.Users
{
    using System;

    public class ReservationViewModel
    {
        public string RealEstateTypeName { get; set; }

        public string DistrictName { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int RealEstateId { get; set; }
    }
}

namespace RentAPlace.Web.ViewModels.Reservation
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ReserveViewModel
    {
        [DataType(DataType.Date)]
        [DisplayName("Reserve From:")]
        public DateTime ReserveFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Reserve To:")]
        public DateTime ReserveTo { get; set; }

        public int RealEstateId { get; set; }
    }
}

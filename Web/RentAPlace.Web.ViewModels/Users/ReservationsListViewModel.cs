namespace RentAPlace.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class ReservationsListViewModel : PagingViewModel
    {
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}

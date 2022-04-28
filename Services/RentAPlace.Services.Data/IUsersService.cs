namespace RentAPlace.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentAPlace.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<IEnumerable<AllUsersViewModel>> All(int page, int itemsPerPage);

        int Count();

        Task PromoteUserById(string id);

        IEnumerable<ReservationViewModel> GetReservations(int page, int itemsPerPage, string userId);

        int ReservationsCount(string userId);
    }
}

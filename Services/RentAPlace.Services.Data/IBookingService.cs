namespace RentAPlace.Services.Data
{
    using System.Threading.Tasks;

    using RentAPlace.Web.ViewModels.Reservation;

    public interface IBookingService
    {
        Task Book(int realEstateId, string userId, ReserveViewModel input);
    }
}

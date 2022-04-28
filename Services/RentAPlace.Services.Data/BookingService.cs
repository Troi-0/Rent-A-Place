using System;
using System.Linq;
using System.Threading;

namespace RentAPlace.Services.Data
{
    using System.Threading.Tasks;

    using RentAPlace.Data.Common.Repositories;
    using RentAPlace.Data.Models;
    using RentAPlace.Web.ViewModels.Reservation;

    public class BookingService : IBookingService
    {
        private readonly IDeletableEntityRepository<RealEstateBooking> bookingRepository;

        public BookingService(IDeletableEntityRepository<RealEstateBooking> bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public async Task Book(int realEstateId, string userId, ReserveViewModel input)
        {
            var bookings = this.bookingRepository.AllAsNoTracking()
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => new
                {
                    x.BookedFrom,
                    x.BookedTo,
                })
                .ToList();

            foreach (var booking in bookings)
            {
                if (input.ReserveFrom >= booking.BookedFrom && input.ReserveFrom <= booking.BookedTo)
                {
                    throw new Exception("Already reserved in this interval.");
                }

                if (input.ReserveTo >= booking.BookedFrom && input.ReserveTo <= booking.BookedTo)
                {
                    throw new Exception("Already reserved in this interval.");
                }

                if (input.ReserveFrom <= booking.BookedFrom && input.ReserveTo >= booking.BookedTo)
                {
                    throw new Exception("Already reserved in this interval.");
                }
            }

            await this.bookingRepository.AddAsync(new RealEstateBooking
            {
                BookedFrom = input.ReserveFrom,
                BookedTo = input.ReserveTo,
                RealEstateId = realEstateId,
                RenterId = userId,
            });

            await this.bookingRepository.SaveChangesAsync();
        }
    }
}

using System;
using RentAPlace.Web.ViewModels.Reservation;

namespace RentAPlace.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Services.Data;

    public class ReservationController : BaseController
    {
        private readonly IBookingService bookingService;

        public ReservationController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public IActionResult Reserve(int id)
        {
            // var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new ReserveViewModel
            {
                RealEstateId = id,
                ReserveFrom = DateTime.UtcNow,
                ReserveTo = DateTime.UtcNow,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.bookingService.Book(input.RealEstateId, userId, input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.View("ThankYouForReservation");
        }
    }
}

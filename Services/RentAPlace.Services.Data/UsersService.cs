namespace RentAPlace.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using RentAPlace.Common;
    using RentAPlace.Data.Common.Repositories;
    using RentAPlace.Data.Models;
    using RentAPlace.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<RealEstateBooking> bookingRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<RealEstateBooking> bookingRepository)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.bookingRepository = bookingRepository;
        }

        public int Count()
        {
            return this.usersRepository.AllAsNoTracking().Count();
        }

        public async Task PromoteUserById(string id)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);
            await this.userManager.AddToRoleAsync(user, GlobalConstants.RentalAgentRoleName);
        }

        public async Task<IEnumerable<AllUsersViewModel>> All(int page, int itemsPerPage)
        {
            var users = this.usersRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new AllUsersViewModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    User = x,
                }).ToList();

            foreach (var user in users)
            {
                user.RolesNames = await this.userManager.GetRolesAsync(user.User);
            }

            return users;
        }

        public IEnumerable<ReservationViewModel> GetReservations(int page, int itemsPerPage, string userId)
        {
            var bookings = this.bookingRepository.AllAsNoTracking()
                .Where(x => x.RenterId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ReservationViewModel
                {
                    DistrictName = x.RealEstate.District.Name,
                    RealEstateId = x.RealEstateId,
                    RealEstateTypeName = x.RealEstate.RealEstateType.Name,
                    From = x.BookedFrom,
                    To = x.BookedTo,
                })
                .ToList();

            return bookings;
        }

        public int ReservationsCount(string userId)
        {
            return this.bookingRepository.All().Count(x => x.RenterId == userId);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RentAPlace.Common;
using RentAPlace.Data.Common.Repositories;
using RentAPlace.Data.Models;
using RentAPlace.Web.ViewModels.Users;

namespace RentAPlace.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
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
    }
}

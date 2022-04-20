using System.Collections.Generic;
using System.Linq;
using RentAPlace.Common;
using RentAPlace.Data.Common.Repositories;
using RentAPlace.Data.Models;
using RentAPlace.Web.ViewModels.Users;

namespace RentAPlace.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public int Count()
        {
            return this.usersRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<AllUsersViewModel> All(int page, int itemsPerPage)
        {
            return this.usersRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new AllUsersViewModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                }).ToList();
        }
    }
}

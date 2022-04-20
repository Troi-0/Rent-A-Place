using RentAPlace.Services.Data;
using RentAPlace.Web.ViewModels.Users;

namespace RentAPlace.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Common;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 12;
            var viewModel = new AllUsersListViewModel
            {
                AllUsers = this.usersService.All(id, itemsPerPage),
                RealEstatesCount = this.usersService.Count(),
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
            };
            return this.View(viewModel);
        }
    }
}

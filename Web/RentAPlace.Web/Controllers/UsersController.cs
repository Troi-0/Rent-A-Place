using System.Threading.Tasks;
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
        public async Task<IActionResult> All(int id = 1)
        {
            const int itemsPerPage = 12;
            var viewModel = new AllUsersListViewModel
            {
                AllUsers = await this.usersService.All(id, itemsPerPage),
                RealEstatesCount = this.usersService.Count(),
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> PromoteById(string id)
        {
            // Todo: use AJAX
            await this.usersService.PromoteUserById(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}

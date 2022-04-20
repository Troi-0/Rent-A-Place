namespace RentAPlace.Services.Data
{
    using System.Collections.Generic;

    using RentAPlace.Web.ViewModels.Users;

    public interface IUsersService
    {
        IEnumerable<AllUsersViewModel> All(int page, int itemsPerPage);

        int Count();
    }
}

namespace RentAPlace.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class AllUsersListViewModel : PagingViewModel
    {
        public IEnumerable<AllUsersViewModel> AllUsers { get; set; }
    }
}

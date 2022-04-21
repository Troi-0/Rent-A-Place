using RentAPlace.Data.Models;

namespace RentAPlace.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<string> RolesNames { get; set; }
    }
}

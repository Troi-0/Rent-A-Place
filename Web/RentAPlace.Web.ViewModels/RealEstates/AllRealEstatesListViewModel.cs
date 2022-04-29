namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System.Collections.Generic;

    public class AllRealEstatesListViewModel : PagingViewModel
    {
        public IEnumerable<AllRealEstatesViewModel> RealEstates { get; set; }

        public SearchViewModel SearchViewModel { get; set; }
    }
}

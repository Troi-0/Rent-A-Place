namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System.ComponentModel;

    public class SearchViewModel
    {
        [DisplayName("Price From:")]
        public int? MinPrice { get; set; }

        [DisplayName("Price To:")]
        public int? MaxPrice { get; set; }

        [DisplayName("District Name:")]
        public string District { get; set; }

        [DisplayName("Size From:")]
        public int? MinSize { get; set; }

        [DisplayName("Size To:")]
        public int? MaxSize { get; set; }
    }
}

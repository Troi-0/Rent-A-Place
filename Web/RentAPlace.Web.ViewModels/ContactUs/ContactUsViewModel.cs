namespace RentAPlace.Web.ViewModels.ContactUs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ContactUsViewModel
    {
        [DisplayName("Email")]
        [EmailAddress]
        public string FromEmail { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}

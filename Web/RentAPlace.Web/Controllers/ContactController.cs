using RentAPlace.Web.ViewModels.ContactUs;

namespace RentAPlace.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        public IActionResult ContactUs()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return null;
            // TODO: redirect to thank you page
        }
    }
}

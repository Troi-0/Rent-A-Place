using System.Threading.Tasks;

namespace RentAPlace.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Services.Messaging;
    using RentAPlace.Web.ViewModels.ContactUs;

    public class ContactController : BaseController
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult ContactUs()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.emailSender.SendEmailAsync(input.FromEmail, input.FromEmail, "rentaplace@abv.bg", input.Title, input.Description);

            return this.View("ThankYou");
        }
    }
}

namespace RentAPlace.Web.Controllers
{
    using System.Diagnostics;

    using RentAPlace.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Services.Data;
    using System.Threading.Tasks;

    public class HomeController : BaseController
    {
        private readonly IRealEstateService realEstateService;

        public HomeController(IRealEstateService realEstateService)
        {
            this.realEstateService = realEstateService;
        }

        public IActionResult Index()
        {
            
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

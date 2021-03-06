namespace RentAPlace.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Common;
    using RentAPlace.Data.Models;
    using RentAPlace.Services.Data;
    using RentAPlace.Web.ViewModels.RealEstates;

    public class RealEstatesController : BaseController
    {
        private readonly IRealEstateService realEstateService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RealEstatesController(
            IRealEstateService realEstateService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.realEstateService = realEstateService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = GlobalConstants.RentalAgentRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public IActionResult AddRealEstate()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.RentalAgentRoleName + "," + GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AddRealEstate(CreateRealEstateViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var path = this.webHostEnvironment.WebRootPath;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.realEstateService.CreateAsync(input, userId, path);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 12;
            var viewModel = new AllRealEstatesListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                RealEstatesCount = this.realEstateService.GetCount(),
                RealEstates = this.realEstateService.All(id, itemsPerPage),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult All(AllRealEstatesListViewModel input, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 12;
            var viewModel = new AllRealEstatesListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                RealEstatesCount = this.realEstateService.GetCountWithSearch(input.SearchViewModel),
                RealEstates = this.realEstateService.AllWithSearch(id, itemsPerPage, input.SearchViewModel),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.realEstateService.ById(id);
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.realEstateService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var model = this.realEstateService.ByIdEdit(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRealEstateViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.realEstateService.UpdateById(input.Id, input);
            return this.RedirectToAction(nameof(this.ById), new { id = input.Id });
        }
    }
}

namespace RentAPlace.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using RentAPlace.Data.Common.Repositories;
    using RentAPlace.Data.Models;
    using RentAPlace.Services.Data;
    using RentAPlace.Web.ViewModels.Settings;

    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : BaseController
    {
        private readonly ISettingsService settingsService;

        private readonly IDeletableEntityRepository<Setting> repository;
        private readonly IRealEstateService realEstateService;

        public SettingsController(
            ISettingsService settingsService,
            IDeletableEntityRepository<Setting> repository,
            IRealEstateService realEstateService)
        {
            this.settingsService = settingsService;
            this.repository = repository;
            this.realEstateService = realEstateService;
        }

        public IActionResult Index()
        {
            var settings = this.settingsService.GetAll<SettingViewModel>();
            var model = new SettingsListViewModel { Settings = settings };
            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            /*await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();*/
            await this.realEstateService.CreateAsync(
            new ViewModels.RealEstates.CreateRealEstateViewModel
            {
                Floor = 3,
                BuildingTypeName = "test",
                DistrictName = "test",
                RealEstateTypeName = "test",
                Rent = 100,
                Size = 100,
                TotalNumberOfFoloors = 10,
                Year = 2020,
            },
            null);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

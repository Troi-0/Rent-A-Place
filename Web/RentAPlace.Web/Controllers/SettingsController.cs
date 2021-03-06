namespace RentAPlace.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RentAPlace.Data.Common.Repositories;
    using RentAPlace.Data.Models;
    using RentAPlace.Services.Data;
    using RentAPlace.Web.ViewModels.Settings;

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

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

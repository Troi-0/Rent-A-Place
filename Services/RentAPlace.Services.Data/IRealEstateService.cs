namespace RentAPlace.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using RentAPlace.Web.ViewModels.RealEstates;

    public interface IRealEstateService
    {
        Task CreateAsync(CreateRealEstateViewModel input, string userId);
    }
}

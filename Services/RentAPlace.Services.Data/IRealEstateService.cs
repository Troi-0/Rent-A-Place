namespace RentAPlace.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using RentAPlace.Web.ViewModels.RealEstates;

    public interface IRealEstateService
    {
        Task CreateAsync(CreateRealEstateViewModel input, string userId, string path);

        IEnumerable<AllRealEstatesViewModel> All(int page, int itemsPerPage);

        int GetCount();

        RealEstateByIdViewModel ById(int id);

        Task DeleteByIdAsync(int id);

        EditRealEstateViewModel ByIdEdit(int id);

        Task UpdateById(int id, EditRealEstateViewModel input);
    }
}

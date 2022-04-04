namespace RentAPlace.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentAPlace.Data.Common.Repositories;
    using RentAPlace.Data.Models;
    using RentAPlace.Web.ViewModels.RealEstates;

    public class RealEstateService : IRealEstateService
    {
        private readonly IDeletableEntityRepository<RealEstate> realEstateRepository;
        private readonly IDeletableEntityRepository<District> districtRepository;
        private readonly IDeletableEntityRepository<BuildingType> buildingTypeRepository;
        private readonly IDeletableEntityRepository<RealEstateType> realEstatetypeRepository;

        public RealEstateService(
            IDeletableEntityRepository<RealEstate> realEstateRepository,
            IDeletableEntityRepository<District> districtRepository,
            IDeletableEntityRepository<BuildingType> buildingTypeRepository,
            IDeletableEntityRepository<RealEstateType> realEstatetypeRepository)
        {
            this.realEstateRepository = realEstateRepository;
            this.districtRepository = districtRepository;
            this.buildingTypeRepository = buildingTypeRepository;
            this.realEstatetypeRepository = realEstatetypeRepository;
        }

        public async Task CreateAsync(CreateRealEstateViewModel input, string userId)
        {
            var realEstate = new RealEstate
            {
                Floor = input.Floor,
                Size = input.Size,
                Rent = input.Rent,
                Year = input.Year,
                TotalNumberOfFloors = input.TotalNumberOfFoloors,
                OwnerId = userId,
            };

            // District
            var districtName = input.DistrictName;

            var district = this.districtRepository.All().FirstOrDefault(x => x.Name == districtName);
            if (district == null)
            {
                district = new District
                {
                    Name = districtName,
                };
            }

            realEstate.District = district;

            // BuildingType
            var buildingTypeName = input.BuildingTypeName;
            var buildingType = this.buildingTypeRepository.All().FirstOrDefault(x => x.Name == buildingTypeName);
            if (buildingType == null)
            {
                buildingType = new BuildingType
                {
                    Name = buildingTypeName,
                };
            }

            realEstate.BuildingType = buildingType;

            // RealEstateType
            var realEstateTypeName = input.RealEstateTypeName;
            var realEstateType = this.realEstatetypeRepository.All().FirstOrDefault(x => x.Name == realEstateTypeName);
            if (realEstateType == null)
            {
                realEstateType = new RealEstateType
                {
                    Name = realEstateTypeName,
                };
            }

            realEstate.RealEstateType = realEstateType;

            await this.realEstateRepository.AddAsync(realEstate);
            await this.realEstateRepository.SaveChangesAsync();
        }
    }
}

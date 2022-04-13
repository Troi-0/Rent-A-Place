using System.IO;

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
        private readonly string[] allowedExtensions = new[] { "jpeg", "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<RealEstate> realEstateRepository;
        private readonly IDeletableEntityRepository<District> districtRepository;
        private readonly IDeletableEntityRepository<BuildingType> buildingTypeRepository;
        private readonly IDeletableEntityRepository<RealEstateType> realEstateTypeRepository;
        private readonly IRepository<Image> imageRepository;

        public RealEstateService(
            IDeletableEntityRepository<RealEstate> realEstateRepository,
            IDeletableEntityRepository<District> districtRepository,
            IDeletableEntityRepository<BuildingType> buildingTypeRepository,
            IDeletableEntityRepository<RealEstateType> realEstateTypeRepository,
            IRepository<Image> imageRepository)
        {
            this.realEstateRepository = realEstateRepository;
            this.districtRepository = districtRepository;
            this.buildingTypeRepository = buildingTypeRepository;
            this.realEstateTypeRepository = realEstateTypeRepository;
            this.imageRepository = imageRepository;
        }

        public async Task CreateAsync(CreateRealEstateViewModel input, string userId, string path)
        {
            var realEstate = new RealEstate
            {
                Floor = input.Floor,
                Size = input.Size,
                Rent = input.Rent,
                Year = input.Year,
                TotalNumberOfFloors = input.TotalNumberOfFloors,
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
            var realEstateType = this.realEstateTypeRepository.All().FirstOrDefault(x => x.Name == realEstateTypeName);
            if (realEstateType == null)
            {
                realEstateType = new RealEstateType
                {
                    Name = realEstateTypeName,
                };
            }

            realEstate.RealEstateType = realEstateType;

            // Images
            Directory.CreateDirectory($"{path}/realEstates/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Contains(extension))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image()
                {
                    Extension = extension,
                };

                realEstate.Images.Add(dbImage);

                var physicalPath = $"{path}/realEstates/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.realEstateRepository.AddAsync(realEstate);
            await this.realEstateRepository.SaveChangesAsync();
        }
    }
}

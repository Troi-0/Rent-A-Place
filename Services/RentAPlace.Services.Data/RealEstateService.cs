namespace RentAPlace.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

        public IEnumerable<AllRealEstatesViewModel> All(int page, int itemsPerPage = 12)
        {
            var realEstatesViewModel = this.realEstateRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new AllRealEstatesViewModel()
                {
                    RealEstateId = x.Id,
                    DistrictName = x.District.Name,
                    Size = x.Size,
                    Rent = x.Rent,
                    RealEstateTypeName = x.RealEstateType.Name,
                    ImageUrl = x.Images.FirstOrDefault().RemoteImageUrl != null
                        ? x.Images.FirstOrDefault().RemoteImageUrl
                        : "/realEstates/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension,
                })
                .ToList();
            return realEstatesViewModel;
        }

        public RealEstateByIdViewModel ById(int id)
        {
            var realEstate = this.realEstateRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => new RealEstateByIdViewModel
                {
                    Id = x.Id,
                    BuildingTypeName = x.BuildingType.Name,
                    DistrictName = x.District.Name,
                    Floor = x.Floor,
                    RealEstateTypeName = x.RealEstateType.Name,
                    Rent = x.Rent,
                    Size = x.Size,
                    Year = x.Year.Value.Year,
                    TotalNumberOfFloors = x.TotalNumberOfFloors,
                })
                .FirstOrDefault();

            var imagesDbList = this.imageRepository
                .All()
                .Where(x => x.RealEstateId == id)
                .Select(x => new
                {
                    x.RemoteImageUrl,
                    x.Extension,
                    x.Id,
                })
                .ToList();

            var images = new List<string>();
            foreach (var imageDb in imagesDbList)
            {
                if (imageDb.RemoteImageUrl != null)
                {
                    images.Add(imageDb.RemoteImageUrl);
                }
                else
                {
                    images.Add("/realEstates/" + imageDb.Id + "." + imageDb.Extension);
                }
            }

            realEstate.ImageUrls = images;

            return realEstate;
        }

        public EditRealEstateViewModel ByIdEdit(int id)
        {
            var realEstate = this.realEstateRepository.All()
                .Where(x => x.Id == id)
                .Select(x => new EditRealEstateViewModel
                {
                    Id = x.Id,
                    BuildingType = x.BuildingType.Name,
                    DistrictName = x.District.Name,
                    Floor = x.Floor,
                    TotalNumberOfFloors = x.TotalNumberOfFloors,
                    Rent = x.Rent,
                    Year = (DateTime)x.Year,
                    Type = x.RealEstateType.Name,
                    Size = x.Size,
                })
                .FirstOrDefault();

            return realEstate;
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

        public async Task DeleteByIdAsync(int id)
        {
            var realEstate = this.realEstateRepository
                .All()
                .First(x => x.Id == id);
            this.realEstateRepository.Delete(realEstate);
            await this.realEstateRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.realEstateRepository.AllAsNoTracking().Count();
        }

        public async Task UpdateById(int id, EditRealEstateViewModel input)
        {
            var realEstate = this.realEstateRepository.All()
                .FirstOrDefault(x => x.Id == id);

            realEstate.Floor = input.Floor;
            realEstate.TotalNumberOfFloors = input.TotalNumberOfFloors;
            realEstate.Rent = input.Rent;
            realEstate.Size = input.Size;
            realEstate.Year = Convert.ToDateTime(input.Year);

            // Building type edit

            var buildingTypeName = input.BuildingType;
            var buildingType = this.buildingTypeRepository.All().FirstOrDefault(x => x.Name == buildingTypeName);
            if (buildingType == null)
            {
                buildingType = new BuildingType
                {
                    Name = buildingTypeName,
                };
            }

            realEstate.BuildingType = buildingType;

            // District edit
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


            // Real estate type edit
            var realEstateTypeName = input.Type;
            var realEstateType = this.realEstateTypeRepository.All().FirstOrDefault(x => x.Name == realEstateTypeName);
            if (realEstateType == null)
            {
                realEstateType = new RealEstateType
                {
                    Name = realEstateTypeName,
                };
            }

            realEstate.RealEstateType = realEstateType;

            await this.realEstateRepository.SaveChangesAsync();
        }
    }
}

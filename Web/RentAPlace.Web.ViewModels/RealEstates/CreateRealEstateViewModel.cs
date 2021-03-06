using RentAPlace.Web.ViewModels.ValidationAttributes;

namespace RentAPlace.Web.ViewModels.RealEstates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    using Microsoft.AspNetCore.Http;

    public class CreateRealEstateViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The size cant be negative.")]
        public int Size { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The floor cant be negative.")]
        public int Floor { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The total number of floors cant be negative.")]
        [DisplayName("Total Number Of Floors")]
        public int TotalNumberOfFloors { get; set; }

        [Required]
        [YearValidation(1800)]
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Rent must be between 1 and 10 000 euros.")]
        [DisplayName("Rent (in euros)")]
        public int Rent { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The district name must have at least 3 symbols.")]
        [DisplayName("District Name")]
        public string DistrictName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The real estate type must have at least 3 symbols.")]
        [DisplayName("Real Estate Type")]
        public string RealEstateTypeName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The building type must have at least 2 symbols.")]
        [DisplayName("Building Type")]
        public string BuildingTypeName { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}

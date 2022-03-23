namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class BuildingType : BaseDeletableModel<int>
    {
        public BuildingType()
        {
            this.RealEstates = new HashSet<RealEstate>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<RealEstate> RealEstates { get; set; }
    }
}

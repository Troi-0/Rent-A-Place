namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Tags = new HashSet<RealEstateTag>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<RealEstateTag> Tags { get; set; }
    }
}

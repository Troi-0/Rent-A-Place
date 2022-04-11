using System;
using System.Collections.Generic;

#nullable disable

namespace WebScraper.Models
{
    public partial class RealEstateType
    {
        public RealEstateType()
        {
            RealEstates = new HashSet<RealEstate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<RealEstate> RealEstates { get; set; }
    }
}

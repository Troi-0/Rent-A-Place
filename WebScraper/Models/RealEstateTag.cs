using System;
using System.Collections.Generic;

#nullable disable

namespace WebScraper.Models
{
    public partial class RealEstateTag
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int TagId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual RealEstate Property { get; set; }
        public virtual Tag Tag { get; set; }
    }
}

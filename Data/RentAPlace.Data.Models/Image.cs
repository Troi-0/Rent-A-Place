namespace RentAPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RentAPlace.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int RealEstateId { get; set; }

        public virtual RealEstate RealEstate { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }
    }
}

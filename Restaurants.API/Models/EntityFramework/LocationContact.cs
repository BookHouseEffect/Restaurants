using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class LocationContact : BaseEntity
    {

        [Required]
        public int Floor { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Locality { get; set; }

        public string AdministrativeAreaLevel2 { get; set; }

        public string AdministrativeAreaLevel1 { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public long LocationPointId { get; set; }

        public string GoogleLink { get; set; }

        [Required]
        public long RestaurantId { get; set; }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

        [ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurantOwningTheSchedule
        {
            get
            {
                return _Restaurant;
            }
            set
            {
                _Restaurant = value;
            }
        }

        [ForeignKey("LocationPointId")]
        public LocationPoints TheLocationPoint { get; set; }
    }
}

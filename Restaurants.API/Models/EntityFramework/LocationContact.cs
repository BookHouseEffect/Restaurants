using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class LocationContact : BaseEntity
    {

        [Required]
        [Column(Order = 1)]
        public int Floor { get; set; }

        [Required]
        [Column(Order = 2)]
        public string StreetNumber { get; set; }

        [Required]
        [Column(Order = 3)]
        public string Route { get; set; }

        [Required]
        [Column(Order = 4)]
        public string Locality { get; set; }

        [Column(Order = 5)]
        public string AdministrativeAreaLevel2 { get; set; }

        [Column(Order = 6)]
        public string AdministrativeAreaLevel1 { get; set; }

        [Required]
        [Column(Order = 7)]
        public string Country { get; set; }

        [Required]
        [Column(Order = 8)]
        public int ZipCode { get; set; }

        [Column(Order = 9)]
        public string GoogleLink { get; set; }

        [Required]
        [Column(Order = 10)]
        public long LocationPointId { get; set; }

        [Required]
        [Column(Order = 11)]
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

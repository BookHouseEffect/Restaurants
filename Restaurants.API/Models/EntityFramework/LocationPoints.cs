using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    [ComplexType]
    public class LocationPoints : BaseEntity
    {
        [Required]
        [Range(minimum: -90.0, maximum: 90.0)]
        [Column(Order = 1)]
        public float Latitude { get; set; }

        [Required]
        [Range(minimum: -180.0, maximum: 180.0)]
        [Column(Order = 2)]
        public float Longitude { get; set; }
    }
}

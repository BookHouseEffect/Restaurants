using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
	[Serializable]
    public class EmployersRestaurants : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long EmployerId { get; set; }

        [Required]
        [Column(Order = 2)]
        public long RestaurantId { get; set; }

        private Employers _Employer;

        [ForeignKey("EmployerId")]
        public virtual Employers TheEmployer
        {
            get
            {
                return _Employer;
            }
            set
            {
                _Employer = value;
            }
        }

        private RestaurantObjects _Restaurant;

        [ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurant
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
    }
}

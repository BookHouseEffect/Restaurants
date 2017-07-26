using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Menus : BaseEntity
    {
        [Required]
        public long RestaurantId { get; set; }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

        [ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurantOwningTheNumber
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

        [NonSerialized]
        private ICollection<MenuLanguages> _MenuLanguage;

        public virtual ICollection<MenuLanguages> TheMenuLanguagesAssosiatedWithThisMenu
        {
            get
            {
                return _MenuLanguage;
            }
            set
            {
                _MenuLanguage = value;
            }
        }

        [NonSerialized]
        private ICollection<MenuItems> _Items;

        public virtual ICollection<MenuItems> TheMenuItems
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
            }
        }
    }
}

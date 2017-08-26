using Newtonsoft.Json;
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
        [Column(Order = 1)]
        public long RestaurantId { get; set; }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

		[JsonIgnore]
		[ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurantOwningTheMenu
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

		[JsonIgnore]
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

		[JsonIgnore]
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

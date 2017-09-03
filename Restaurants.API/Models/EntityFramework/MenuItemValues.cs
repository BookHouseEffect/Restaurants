using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuItemValues : BaseEntity
    {
        [Required]
        [DataType(DataType.Currency)]
        [Column(Order = 1)]
        public float Price { get; set; }

        [Required]
        [Column(Order = 2)]
        public long MenuCurrencyId { get; set; }

        [Required]
        [Column(Order = 3)]
        public long MenuItemId { get; set; }

        private MenuCurrencies _Currency;

		[ForeignKey("MenuCurrencyId")]
        public MenuCurrencies TheMenuCurrency
        {
            get
            {
                return _Currency;
            }
            set
            {
                _Currency = value;
            }
        }

        [NonSerialized]
        private MenuItems _Item;

		[JsonIgnore]
		[ForeignKey("MenuItemId")]
        public virtual MenuItems TheMenuItem
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
            }
        }

    }
}

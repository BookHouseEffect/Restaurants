using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuItemValues : BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public float Price { get; set; }

        [Required]
        public long MenuCurrencyId { get; set; }

        [Required]
        public long MenuItemId { get; set; }

        [NonSerialized]
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

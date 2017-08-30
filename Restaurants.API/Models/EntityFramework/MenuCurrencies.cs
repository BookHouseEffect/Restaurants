using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuCurrencies : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long MenuId { get; set; }

        [Required]
        [Column(Order = 1)]
        public long CurrencyId { get; set; }

        [NonSerialized]
        private Menus _Menu;

		[JsonIgnore]
		[ForeignKey("MenuId")]
        public virtual Menus TheMenu
        {
            get
            {
                return _Menu;
            }
            set
            {
                _Menu = value;
            }
        }

        private Currencies _Currency;

		[ForeignKey("CurrencyId")]
        public virtual Currencies TheCurrency
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
        private ICollection<MenuItemValues> _Values;

		[JsonIgnore]
		public virtual ICollection<MenuItemValues> TheMenuCurrencyValues
        {
            get
            {
                return _Values;
            }
            set
            {
                _Values = value;
            }
        }
    }
}

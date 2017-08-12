using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuCategories : BaseEntity
    {

        [NonSerialized]
        private ICollection<MenuItems> _Items;

		[JsonIgnore]
		public virtual ICollection<MenuItems> TheItems
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

        [NonSerialized]
        private ICollection<Categories> _Categories;

		[JsonIgnore]
		public virtual ICollection<Categories> TheCategories
        {
            get
            {
                return _Categories;
            }
            set
            {
                _Categories = value;
            }
        }
    }
}

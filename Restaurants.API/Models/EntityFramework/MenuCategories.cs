using System;
using System.Collections.Generic;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuCategories : BaseEntity
    {

        [NonSerialized]
        private ICollection<MenuItems> _Items;

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

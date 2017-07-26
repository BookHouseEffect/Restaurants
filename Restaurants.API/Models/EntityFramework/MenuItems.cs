using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuItems : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long MenuId { get; set; }

        [Required]
        [Column(Order = 2)]
        public long MenuCategoryId { get; set; }

        [NonSerialized]
        private Menus _Menu;

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

        [NonSerialized]
        private MenuCategories _Category;

        [ForeignKey("MenuCategoryId")]
        public virtual MenuCategories TheMenuCategory
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
            }
        }

        [NonSerialized]
        private ICollection<MenuItemContents> _Content;

        public virtual ICollection<MenuItemContents> TheContent
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }
    }
}

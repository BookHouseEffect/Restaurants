using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuLanguages : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long MenuId { get; set; }

        [Required]
        [Column(Order = 2)]
        public long LanguageId { get; set; }

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

        [NonSerialized]
        private Languages _Language;

		[JsonIgnore]
		[ForeignKey("LanguageId")]
        public virtual Languages TheLanguage
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }

        [NonSerialized]
        private ICollection<Categories> _Categories;

		[JsonIgnore]
		public virtual ICollection<Categories> TheMenuLanguageCategories
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

        [NonSerialized]
        private ICollection<MenuItemContents> _Contents;

		[JsonIgnore]
		public virtual ICollection<MenuItemContents> TheMenuLanguageContents
        {
            get
            {
                return _Contents;
            }
            set
            {
                _Contents = value;
            }
        }
    }
}

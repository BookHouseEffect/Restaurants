using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuItemContents : BaseEntity
    {

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        public string ItemWarnings { get; set; }

        [Required]
        public long MenuItemId { get; set; }

        [Required]
        public long MenuLanguageId { get; set; }

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

        [NonSerialized]
        private MenuLanguages _MenuLanguages;

        [ForeignKey("MenuLanguageId")]
        public virtual MenuLanguages TheMenuLanguage
        {
            get
            {
                return _MenuLanguages;
            }
            set
            {
                _MenuLanguages = value;
            }
        }
    }
}

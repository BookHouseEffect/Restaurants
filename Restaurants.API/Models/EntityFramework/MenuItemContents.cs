using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class MenuItemContents : BaseEntity
    {

        [Required]
        [Column(Order = 1)]
        public string ItemName { get; set; }

        [Required]
        [Column(Order = 2)]
        public string ItemDescription { get; set; }

        [Column(Order = 2)]
        public string ItemWarnings { get; set; }

        [Required]
        [Column(Order = 4)]
        public long MenuItemId { get; set; }

        [Required]
        [Column(Order = 5)]
        public long MenuLanguageId { get; set; }

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

        [NonSerialized]
        private MenuLanguages _MenuLanguages;

		[JsonIgnore]
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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Categories : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public string CategoryName { get; set; }

        [Column(Order = 2)]
        public string CategoryDescription { get; set; }

        [Required]
        [Column(Order = 3)]
        public long MenuCategoryId { get; set; }

        [Required]
        [Column(Order = 4)]
        public long MenuLanguageId { get; set; }

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

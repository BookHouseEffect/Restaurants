using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [Required]
        [ForeignKey("CreatedBy")]
        public long CreatedByUserId { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        [ForeignKey("ModifiedBy")]
        public long ModifiedByUserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedDateTime { get; set; }

        [NonSerialized]
        private People _Created;

        public virtual People CreatedBy
        {
            get
            {
                return _Created;
            }
            set
            {
                _Created = value;
            }
        }

        [NonSerialized]
        private People _Modified;

        public virtual People ModifiedBy
        {
            get
            {
                return _Modified;
            }
            set
            {
                _Modified = value;
            }
        }
    }
}

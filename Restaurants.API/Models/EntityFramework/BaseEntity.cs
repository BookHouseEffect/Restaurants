using Newtonsoft.Json;
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

        [ForeignKey("CreatedBy")]
        public long? CreatedByUserId { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedDateTime { get; set; }

        [ForeignKey("ModifiedBy")]
        public long? ModifiedByUserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ModifiedDateTime { get; set; }

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

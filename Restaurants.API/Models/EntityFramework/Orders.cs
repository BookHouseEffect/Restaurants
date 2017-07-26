using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Orders : BaseEntity
    {
        [Required]
        public float Total { get; set; }

        [Required]
        public long OrderStatusId { get; set; }

        [NonSerialized]
        private OrderStatuses _Status;

        [ForeignKey("OrderStatusId")]
        public virtual OrderStatuses TheStatusForThisOrder
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        [NonSerialized]
        private OrderItems _Items;

        public virtual OrderItems TheItemsForThisOrder
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
    }
}

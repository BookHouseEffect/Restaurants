using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Orders : BaseEntity
    {
        [Required]
        [DataType(DataType.Currency)]
        [Column(Order = 1)]
        public float Total { get; set; }

        [Required]
        [Column(Order = 2)]
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
        private ICollection<OrderItems> _Items;

        public virtual ICollection<OrderItems> TheItemsForThisOrder
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

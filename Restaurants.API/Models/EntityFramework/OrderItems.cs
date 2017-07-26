using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OrderItems : BaseEntity
    {
        [Required]
        public long OrderedItemId { get; set; }

        [Range(minimum: 1, maximum: 1000)]
        [Required]
        public int Count { get; set; }

        [Required]
        public float SubTotal { get; set; }

        [Required]
        public long OrderId { get; set; }

        [Required]
        public long OrderItemStatusId { get; set; }

        [NonSerialized]
        private MenuItems _Item;

        [ForeignKey("OrderedItemId")]
        public virtual MenuItems TheOrderedItem {
            get {
                return _Item;
            }
            set
            {
                _Item = value;
            }
        }

        [NonSerialized]
        private Orders _Order;

        [ForeignKey("OrderId")]
        public virtual Orders TheOrderForThisOrderItem
        {
            get
            {
                return _Order;
            }
            set
            {
                _Order = value;
            }
        }

        [NonSerialized]
        private OrderItemStatuses _Status;

        [ForeignKey("OrderItemStatusId")]
        public virtual OrderItemStatuses TheStatusForThisOrderItem
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
    }
}

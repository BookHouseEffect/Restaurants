using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OrderItems : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long OrderedItemId { get; set; }

        [Range(minimum: 1, maximum: 100)]
        [Required]
        [Column(Order = 2)]
        public int Count { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(Order = 3)]
        public float SubTotal { get; set; }

        [Required]
        [Column(Order = 4)]
        public long OrderId { get; set; }

        [Required]
        [Column(Order = 5)]
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

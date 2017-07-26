using Restaurants.API.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OrderStatuses : BaseEnumEntity
    {
        [Required]
        [Column(Order = 1)]
        public string StatusName { get; set; }

        protected OrderStatuses() { } //For EF

        private OrderStatuses(OrderStatusEnum @enum)
        {
            this.Id = (int)@enum;
            this.StatusName = @enum.ToString();
        }

        public static implicit operator OrderStatuses(OrderStatusEnum @enum)
            => new OrderStatuses(@enum);

        public static implicit operator OrderStatusEnum(OrderStatuses status)
            => (OrderStatusEnum)status.Id;
    }
}

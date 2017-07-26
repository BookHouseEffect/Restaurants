using Restaurants.API.Models.Enums;
using System;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OrderItemStatuses : BaseEnumEntity
    {
        public string StatusName { get; set; }

        protected OrderItemStatuses() { } //For EF

        private OrderItemStatuses(OrderItemStatusEnum @enum)
        {
            this.Id = (int)@enum;
            this.StatusName = @enum.ToString();
        }

        public static implicit operator OrderItemStatuses(OrderItemStatusEnum @enum)
            => new OrderItemStatuses(@enum);

        public static implicit operator OrderItemStatusEnum(OrderItemStatuses status)
            => (OrderItemStatusEnum)status.Id;
    }
}

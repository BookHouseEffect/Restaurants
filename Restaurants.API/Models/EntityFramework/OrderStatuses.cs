using Restaurants.API.Models.Enums;
using System;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OrderStatuses : BaseEnumEntity
    {
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

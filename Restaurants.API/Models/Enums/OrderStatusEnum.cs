namespace Restaurants.API.Models.Enums
{
    public enum OrderStatusEnum
    {
        ORDER_INITIALIZED = 1, 
        ORDER_SEND_FOR_PREPAIRING,
        ORDER_PREPAIRED,
        ORDER_EXPANDING,
        ORDER_SEND_FOR_CLOSING, 
        ORDER_CLOSED,
        ORDER_PAYED
    }
}

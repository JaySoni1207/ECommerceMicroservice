using System.Text.Json.Serialization;

namespace Ecommerce.Common.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,      // Order created, waiting for processing
    Processing,   // Being processed
    Shipped,      // Order has been shipped
    Delivered,    // Order delivered to the customer
    Cancelled     // Order cancelled by user/admin
}
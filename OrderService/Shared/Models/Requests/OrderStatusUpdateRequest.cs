using Ecommerce.Common.Shared.Enums;

namespace OrderService.Shared.Models.Requests;

public class OrderStatusUpdateRequest
{
    public OrderStatus Status { get; set; }
}
using Ecommerce.Common.Shared.Enums;

namespace OrderService.Shared.Models.Responses;

public class OrderDetailResponse
{
    public string OrderReference { get; set; }
    public string CustomerName { get; set; }  
    public decimal TotalAmount { get; set; }  
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
}
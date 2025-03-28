using System.ComponentModel.DataAnnotations;
using Ecommerce.Common.Shared.Enums;
using Ecommerce.Common.Shared.Methods;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Shared.Models;

[Index(nameof(OrderReference), Name = "unique_order_reference", IsUnique = true)]
[Index(nameof(MobileNumber), Name = "unique_mobile_number", IsUnique = true)]
public class Order
{
    public int Id { get; set; } 
    [MaxLength(20)]
    public string OrderReference { get; set; } = OrderServiceHelper.GenerateOrderReference(); // Public unique identifier
    public string CustomerName { get; set; }  
    public decimal TotalAmount { get; set; }  
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [MaxLength(10)]
    public long MobileNumber { get; set; }
    public int ProductId { get; set; }
}
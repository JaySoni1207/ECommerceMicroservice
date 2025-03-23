using Ecommerce.Common.Shared.Enums;
using MediatR;
using OrderService.Shared.Data;
using OrderService.Shared.Models;
using OrderService.Shared.Models.Application.Commands;

namespace OrderService.Application.Handlers;
public class CreateOrderHandler(OrderDbContext dbContext) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerName = request.CustomerName,
            TotalAmount = request.TotalAmount,
            MobileNumber = request.MobileNumber,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow
        };

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();

        return order;
    }
}
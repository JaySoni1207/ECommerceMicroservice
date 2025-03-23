using Ecommerce.Common.Shared.Enums;
using Mapster;
using MediatR;
using OrderService.Infrastructure.Data;
using OrderService.Shared.Models;
using OrderService.Shared.Models.Application.Commands;
using OrderService.Shared.Models.Responses;

namespace OrderService.Application.Handlers;
public class CreateOrderHandler(OrderDbContext dbContext) : IRequestHandler<CreateOrderCommand, OrderDetailResponse>
{
    public async Task<OrderDetailResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

        return order.Adapt<OrderDetailResponse>();
    }
}
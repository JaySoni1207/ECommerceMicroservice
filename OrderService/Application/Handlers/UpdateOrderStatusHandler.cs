using Ecommerce.Common.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using OrderService.Shared.Models.Application.Commands;

namespace OrderService.Application.Handlers;
public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly OrderDbContext _dbContext;

    public UpdateOrderStatusHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(i => i.Id == request.OrderId, cancellationToken: cancellationToken);
        if (order == null)
            return false;

        if (!IsValidStatusTransition(order.Status, request.NewStatus))
            return false;

        order.Status = request.NewStatus;
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return currentStatus switch
        {
            OrderStatus.Pending => newStatus is OrderStatus.Processing or OrderStatus.Cancelled,
            OrderStatus.Processing => newStatus == OrderStatus.Shipped,
            OrderStatus.Shipped => newStatus == OrderStatus.Delivered,
            _ => false
        };
    }
}
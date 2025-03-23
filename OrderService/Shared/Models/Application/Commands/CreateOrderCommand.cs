using MediatR;

namespace OrderService.Shared.Models.Application.Commands;

public record CreateOrderCommand(string CustomerName, decimal TotalAmount, int MobileNumber) : IRequest<Order>;

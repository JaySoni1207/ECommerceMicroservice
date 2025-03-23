using MediatR;
using OrderService.Shared.Models.Responses;

namespace OrderService.Shared.Models.Application.Commands;

public record CreateOrderCommand(string CustomerName, decimal TotalAmount, long MobileNumber) : IRequest<OrderDetailResponse>;

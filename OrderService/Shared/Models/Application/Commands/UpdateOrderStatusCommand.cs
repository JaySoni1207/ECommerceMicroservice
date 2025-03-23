using Ecommerce.Common.Shared.Enums;
using MediatR;

namespace OrderService.Shared.Models.Application.Commands;

public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<bool>;

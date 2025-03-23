using MediatR;
using OrderService.Shared.Models.Responses;

namespace OrderService.Shared.Models.Application.Queries;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDetailResponse>;
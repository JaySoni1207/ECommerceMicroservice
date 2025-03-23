using System.Globalization;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OrderService.Infrastructure.Data;
using OrderService.Shared.Models;
using OrderService.Shared.Models.Application.Queries;
using OrderService.Shared.Models.Responses;

namespace OrderService.Application.Handlers;
public class GetOrderByIdHandler(OrderDbContext dbContext) : IRequestHandler<GetOrderByIdQuery, OrderDetailResponse>
{
    public async Task<OrderDetailResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderDetail = await dbContext.Orders.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken) ?? throw new NpgsqlException();
        return orderDetail.Adapt<OrderDetailResponse>();
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Shared.Models.Application.Commands;
using OrderService.Shared.Models.Application.Queries;
using OrderService.Shared.Models.Requests;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase  
{  
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)  
    {  
        var order = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderReference }, order);
    }  

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)  
    {  
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return order != null ? Ok(order) : NotFound();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderStatusCommand(id, request.Status));

        if (!result)
            return BadRequest("Invalid status transition or order not found");

        return Ok(new { Message = "Order status updated successfully" });
    }
}   
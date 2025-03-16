using FinalLabTask1.CQRS;
using FinalLabTask1.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace FinalLabTask1.Controllers;

public class EventController: ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> DispatchEvent([FromBody] DispatchEventCommand command)
    {
        await _mediator.Send(command);

        return Ok("Success in dispatching event");
    }

    [HttpGet("events/{transactionId}")]
    public async Task<ActionResult> GetEventsByTransactionId(long transactionId)
    {
        var results = await _mediator.Send(new GetAllEventsQuery(transactionId));
        
        return Ok(results);
    }
}
    

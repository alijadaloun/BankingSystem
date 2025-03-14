using FinalLabTask1.CQRS;
using FinalLabTask1.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

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

        return Ok("Success");
    }

    [HttpGet("events/{transactionId}")]
    public async Task<ActionResult<List<TransactionEvent>>> GetEventsByTransactionId(long transactionId)
    {
        var results = _mediator.Send(new GetAllEventsQuery(transactionId));
        return Ok(results);
    }
}
    
}
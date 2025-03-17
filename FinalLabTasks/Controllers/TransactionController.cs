using FinalLabTask1.Services;
using FinalLabTask1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabTask1.Controllers;
[ApiController]
[Route("api/transactions")]
public class TransactionController:ControllerBase
{
    private readonly TransferService _transferService;

    public TransactionController(TransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost("notify")]
    public async Task<IActionResult> NotifyTransaction([FromBody] TransactionNotifyRequest request)
    {
        var message = await _transferService.NotifyTransactionAsync(request.TransactionId);
        return Ok(message);
    }
    public class TransactionNotifyRequest
    {
        public int TransactionId { get; set; }
    }
    
}
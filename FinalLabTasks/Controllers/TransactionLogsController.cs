using FinalLabTask1.Entities;
using FinalLabTask1.Services;
using FinalLabTask1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace FinalLabTask1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TransactionLogsController: ControllerBase
{
    private readonly ILogger<TransactionLogsController> _logger;
    private readonly ITransactionLogsService _transactionLogsService;

    public TransactionLogsController(ITransactionLogsService transactionLogsService,
        ILogger<TransactionLogsController> logger)
    {
        _transactionLogsService = transactionLogsService;
        _logger = logger;
    }

    [HttpPost("transaction-logs")]
    public async Task<ActionResult<TransactionLogs>> PostTransactionLogs(int accountId, TransactionType transactionType, double amount, Status status,
        string details)
    {
        var result = await _transactionLogsService.PostTransactionLogs(accountId, transactionType, amount, status, details);
        

        return Ok(result);
    }

    [HttpGet("transaction-logs/{accountid}")]
    public async Task<ActionResult<TransactionLogs>> GetTransactionLogs(int accountid)
    {
        var result = await _transactionLogsService.GetTransactionLogs(accountid);
        return Ok(result);
    }
    
    
}
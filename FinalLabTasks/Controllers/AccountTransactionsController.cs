using FinalLabTask1.Entities;
using FinalLabTask1.Services;
using FinalLabTask1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabTask1.Controllers;
[ApiController]
[Route("api/accounts")]
public class AccountTransactionsController: ControllerBase
{
    private readonly ITransactionLogsService _transactionLogsService;
    private readonly ITransferService _transferService;

    public AccountTransactionsController(ITransactionLogsService transactionLogsService, ITransferService transferService)
    {
        _transactionLogsService = transactionLogsService;
        _transferService = transferService;
    }

    [HttpGet("common-transactions")]
    public async Task<IEnumerable<AccountTransactions>> GetCommonTransactions([FromBody]List<int> accountIds)
    {
        var results = _transactionLogsService.GetCommonTransactions( accountIds);
        return await results;
    }

    [HttpGet("balance-summary/{userId}")]
    public async Task<IEnumerable<AccountBalanceSummary>> GetAccountBalanceSummary(int userId)
    {
        var results = _transactionLogsService.GetAccountBalanceSummary(userId);
        return await results;
    }
    [HttpPost("transfer")]
    public async Task<ActionResult> PostTransfer([FromBody] Account fromAccount, [FromBody] Account toAccount, decimal amount)
    {
        var result = await _transferService.Transfer(fromAccount, toAccount, amount);
        return result?Ok("Transfer successful"):Ok("Transfer failed");
        
    }
}
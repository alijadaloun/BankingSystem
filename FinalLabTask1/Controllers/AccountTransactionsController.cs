using FinalLabTask1.Entities;
using FinalLabTask1.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabTask1.Controllers;
[ApiController]
[Route("accounts")]
public class AccountTransactionsController
{
    private readonly ITransactionLogsService _transactionLogsService;

    public AccountTransactionsController(ITransactionLogsService transactionLogsService)
    {
        _transactionLogsService = transactionLogsService;
    }

    [HttpGet("common-transactions")]
    public async Task<IEnumerable<AccountTransactions>> GetCommonTransactions([FromBody]List<int> accountIds)
    {
        var results = _transactionLogsService.GetCommonTransactions( accountIds);
        return await results;
    }

    [HttpGet("balance-summary/{userId")]
    public async Task<IEnumerable<AccountBalanceSummary>> GetAccountBalanceSummary(int userId)
    {
        var results = _transactionLogsService.GetAccountBalanceSummary(userId);
        return await results;
    }
}
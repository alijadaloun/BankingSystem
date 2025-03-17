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
    private readonly IAccountService _accountService;

    public AccountTransactionsController(ITransactionLogsService transactionLogsService, ITransferService transferService, IAccountService accountService)
    {
        
        _transactionLogsService = transactionLogsService;
        _transferService = transferService;
        _accountService = accountService;
    }

    [HttpGet("common-transactions")]
    public async Task<IEnumerable<AccountTransactions>> GetCommonTransactions([FromBody]List<int> accountIds)
    {
        var results = _transactionLogsService.GetCommonTransactions( accountIds);
        return await results;
    }

    [HttpGet("balance-summary/{userId}")]
    public async Task<IEnumerable<AccountBalanceSummary>> GetAccountBalanceSummary([FromRoute] int userId)
    {
        var results = _transactionLogsService.GetAccountBalanceSummary(userId);
        return await results;
    }
    [HttpPost("transfer")]
    public async Task<ActionResult> PostTransfer([FromBody] TransferRequestDTO transferRequestDto)
    {
        var result = await _transferService.Transfer(transferRequestDto.FromAccount, transferRequestDto.ToAccount, transferRequestDto.Amount);
        return result?Ok("Transfer successful"):Ok("Transfer failed");
        
    }

    [HttpGet("{accountId}/details")]
    public async Task<ActionResult> GetAccountDetails(int accountId)
    {
       var account =  await _accountService.GetAccountDetails(accountId);
       return Ok(account);

    }
    public class TransferRequestDTO
    {
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
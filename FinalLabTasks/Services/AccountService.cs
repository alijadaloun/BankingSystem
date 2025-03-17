using FinalLabTask1.Entities;
using FinalLabTask1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FinalLabTask1.Services;

public class AccountService: IAccountService
{
    private readonly TransactionDbContext _context;
    private readonly IStringLocalizer<AccountService> _localizer;

    public AccountService(TransactionDbContext context, IStringLocalizer<AccountService> localizer)
    {
        _context = context;
        _localizer = localizer;
    }
    public async Task<LocalizedAccountDetails> GetAccountDetails(int accountId)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountId == accountId);
        if (account == null) throw new Exception("Account not found");
        var local = new LocalizedAccountDetails
        {
            AccountId = account.AccountId,
            AccountType = _localizer[account.AccountType],
            Balance = account.AccountBalance,
            WelcomeMessage = _localizer["WelcomeMessage"]

        };
        return local;
    }

}

public class LocalizedAccountDetails
{
    public int AccountId { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public string WelcomeMessage { get; set; }
}
using FinalLabTask1.Entities;
using FinalLabTask1.Services.Interfaces;
using Microsoft.Extensions.Localization;

namespace FinalLabTask1.Services;

public class TransferService: ITransferService
{
    private readonly TransactionDbContext _context;
    private readonly IStringLocalizer<TransferService> _localizer;
    public TransferService(TransactionDbContext context, IStringLocalizer<TransferService> localizer)
    {
        _context = context;
        _localizer = localizer;
    }

    public async Task<bool> Transfer(Account fromAccount, Account toAccount, decimal amount)
    {
        try
        {
            Transaction t1 = new Transaction
            {
                TransactionAmount = amount,
                TransactionDate = DateTime.Now,
                SenderId = fromAccount.AccountId,
                ReceiverId = toAccount.AccountId,
            };
            await _context.AddAsync(t1);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
        
    }
    
    public async Task<string> NotifyTransactionAsync(int transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction == null)
        {
            return "No transaction found";
        }
        var message = string.Format(_localizer["TransactionNotification"], transactionId);
        return message;
    }

}
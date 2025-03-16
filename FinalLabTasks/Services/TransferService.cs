using FinalLabTask1.Entities;
using FinalLabTask1.Services.Interfaces;

namespace FinalLabTask1.Services;

public class TransferService: ITransferService
{
    private readonly TransactionDbContext _context;
    public TransferService(TransactionDbContext context)
    {
        _context = context;
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
}
using FinalLabTask1.Entities;

namespace FinalLabTask1.Services;

public interface ITransactionLogsService
{
    public  Task<TransactionLogs> PostTransactionLogs(int accountId, TransactionType transactionType, double amount,Status status, string details);
    //AccountId, TransactionType, Amount, Status, Details.

    public Task<List<TransactionLogs>> GetTransactionLogs(int accountId);
    public Task<IEnumerable<TransactionLogs>> Get();
    
    
    
}
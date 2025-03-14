using System.Text;
using RabbitMQ.Client;
using FinalLabTask1.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalLabTask1.Services;

public class TransactionLogService: ITransactionLogsService
{
    private readonly TransactionDbContext _context;

    public TransactionLogService(TransactionDbContext context)
    {
        _context = context;
    }
    public async Task<TransactionLogs> PostTransactionLogs(int accountId, TransactionType transactionType, double amount, Status status,
        string details)
    {//now this send the transaction to rabbitmq queue, then another client can retrieve it
        var transactionLogs = new TransactionLogs
        {
            AccountId = accountId,
            TransactionType = transactionType,
            Amount = amount,
            Status = status,
            Details = details
        };


        var factory = new ConnectionFactory{HostName = "localhost"};
        await using( var connection = await factory.CreateConnectionAsync())
        await using (var channel = await connection.CreateChannelAsync())
        {
            await channel.QueueDeclareAsync(queue: "RabbitQueue", durable: true, exclusive: false, autoDelete: false,
                arguments: null);
            var message = JsonConvert.SerializeObject(transactionLogs);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "", body: body, basicProperties: new BasicProperties(), mandatory: true);
            //ready to be consumed by a consumer service
        }
            
        
        await _context.TransactionLogs.AddAsync(transactionLogs);
        await _context.SaveChangesAsync();
        
        
        return transactionLogs;
        
    }
    

    public async Task<List<TransactionLogs>> GetTransactionLogs(int accountId)
    {
        if( accountId <0) throw new Exception("Invalid account Id ");
        var result = _context.TransactionLogs.Where(x => x.AccountId == accountId).ToList();
        if(result.Count == 0) throw new Exception($"No account of id {accountId} found");
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<AccountTransactions>> GetCommonTransactions(List<int> accountIds)
    {
        var s = await _context.AccountTransactions.ToListAsync();
        var transactionQuery = from x in s
            where accountIds.Contains(x.AccountId)
                select x;
        return  transactionQuery.ToList();

    }

    public async Task<IEnumerable<AccountBalanceSummary>> GetAccountBalanceSummary(int userId)
    {
        var transactions = await _context.AccountTransactions
            .Where(t => t.AccountId == userId)
            .ToListAsync();
        var summary = transactions
            .GroupBy(t => t.AccountId)
            .Select(g => new AccountBalanceSummary
            {
                AccountId = g.Key,
                TotalDeposits = g.Where(t => t.TransactionType == TransactionType.Deposit).Sum(t => t.Amount),
                TotalWithdrawals = g.Where(t => t.TransactionType == TransactionType.Withdrawal).Sum(t => t.Amount),
                TotalBalance = g.Sum(t => t.TransactionType == TransactionType.Deposit ? t.Amount : -t.Amount)
            })
            .ToList();
        return summary.AsEnumerable();
        
    }

    public async Task<IEnumerable<AccountTransactions>> Get()
    {
        var result =  _context.AccountTransactions.AsEnumerable();
        return await Task.FromResult(result);
        

    }
}
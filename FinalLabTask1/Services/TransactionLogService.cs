using System.Text;
using RabbitMQ.Client;
using FinalLabTask1.Entities;
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
    {
        var transactionLogs = new TransactionLogs
        {
            AccountId = accountId,
            TransactionType = transactionType,
            Amount = amount,
            Status = status,
            Details = details
        };


        var factory = new ConnectionFactory{HostName = "localhost"};
        using( var connection = await factory.CreateConnectionAsync())
        using (var channel = await connection.CreateChannelAsync())
        {
            channel.QueueDeclareAsync(queue: "RabbitQueue", durable: true, exclusive: false, autoDelete: false,
                arguments: null);
            var message = JsonConvert.SerializeObject(transactionLogs);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "", body: body, basicProperties: new BasicProperties(), mandatory: true);
            //ready to be consumed by a consumer service
            return transactionLogs;

        }
            
        
        await _context.TransactionLogs.AddAsync(transactionLogs);
        await _context.SaveChangesAsync();
        
        
        return transactionLogs;
        
    }
    

    public async Task<List<TransactionLogs>> GetTransactionLogs(int accountId)
    {
        if( accountId <0) throw new Exception("Invalid account Id ");
        var result = _context.TransactionLogs.Where(x => x.AccountId == accountId).ToList();
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<TransactionLogs>> Get()
    {
        var result =  _context.TransactionLogs.AsEnumerable();
        return await Task.FromResult(result);
        

    }
}
using System.Windows.Input;
using FinalLabTask1.Entities;
using MediatR;

namespace FinalLabTask1.CQRS;
public record DispatchEventCommand(long TransactionId, string EventType, string Details, DateTime Timestamp) : IRequest<bool>;
public class DispatchEventCommandHandler: IRequestHandler<DispatchEventCommand, bool>
{ 
    private readonly  TransactionDbContext _context;

    public DispatchEventCommandHandler(TransactionDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DispatchEventCommand request, CancellationToken cancellationToken)
    {
        var transactionEvent = new TransactionEvent
        {
            TransactionId = request.TransactionId,
            EventType = request.EventType,
            Details = request.Details,
            Timestamp = request.Timestamp
        };

        await _context.TransactionEvent.AddAsync(transactionEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return true;

    }
    
}
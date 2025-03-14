using FinalLabTask1.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalLabTask1.CQRS;
public record GetAllEventsQuery(long TransactionId) : IRequest<List<TransactionEvent>>;
public class GetAllEventsQueryHandler:IRequestHandler<GetAllEventsQuery, List<TransactionEvent>>
{
private readonly TransactionDbContext _context;

public GetAllEventsQueryHandler(TransactionDbContext context)
{
    _context = context;
}



public Task<List<TransactionEvent>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
{
    return _context.TransactionEvent
        .Where(e => e.TransactionId == request.TransactionId)
        .ToListAsync(cancellationToken);
}
}
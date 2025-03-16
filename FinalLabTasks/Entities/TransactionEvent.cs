using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalLabTask1.Entities;

public class TransactionEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long TransactionId { get; set; }

    public string EventType { get; set; }

    public string Details { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
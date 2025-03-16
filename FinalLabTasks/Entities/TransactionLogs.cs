using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalLabTask1.Entities;

public class TransactionLogs
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }
    public DateTime TimeStamp { get; set; }
    public Status Status { get; set; }
    public string Details { get; set; } ="";

    

}
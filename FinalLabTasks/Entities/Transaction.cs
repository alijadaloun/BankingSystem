using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalLabTask1.Entities;

public class Transaction
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
    public decimal TransactionAmount { get; set; }
    public int SenderId{ get; set; }
    public int ReceiverId{ get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();
    
}
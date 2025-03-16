using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalLabTask1.Entities;

public class AccountTransactions
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

public class AccountBalanceSummary
{
    public int AccountId { get; set; }
    public double TotalDeposits { get; set; }
    public double TotalWithdrawals { get; set; }
    public double TotalBalance { get; set; }
}
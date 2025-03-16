using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalLabTask1.Entities;

public class Account
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int AccountId { get; set; }
    public string Name { get; set; }
    public decimal AccountBalance { get; set; }
    public int TransactionId { get; set; } = 0;
    public Transaction Transaction { get; set; }

}
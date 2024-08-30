namespace Data.Models;
public class BankAccount
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; set; }
    public ICollection<Transaction> FromTransactions { get; set; }
    public ICollection<Transaction> ToTransactions { get; set; }
}

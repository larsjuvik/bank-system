namespace Data.Models;
public class BankAccount
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool HasDebitCard { get; set; } = false;
    public BankAccountType AccountType { get; set; }

    public User Owner { get; set; }
    public ICollection<Transaction> FromTransactions { get; set; }
    public ICollection<Transaction> ToTransactions { get; set; }

    public enum BankAccountType
    {
        Checking = 0,
        Savings = 1,
        CD = 2,
        Investing = 3
    }
}

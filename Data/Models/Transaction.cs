namespace Data.Models;
public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }

    public int FromId { get; set; }
    public BankAccount From { get; set; }

    public int ToId { get; set; }
    public BankAccount To { get; set; }
}

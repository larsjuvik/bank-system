namespace Data.Models;
public class BankAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}

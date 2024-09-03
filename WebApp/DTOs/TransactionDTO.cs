namespace WebApp.DTOs;
public class TransactionDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public BankAccountDTO From { get; set; }
    public BankAccountDTO To { get; set; }
}
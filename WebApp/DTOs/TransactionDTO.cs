namespace WebApp.DTOs;
public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public BankAccountDto From { get; set; }
    public BankAccountDto To { get; set; }
}
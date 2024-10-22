namespace WebApp.DTOs;

public class TransactionDto
{ 
    public required decimal Amount { get; init; }
    public required DateTime TransactionDate { get; init; }
    public required BankAccountDto From { get; init; }
    public required BankAccountDto To { get; init; }
}
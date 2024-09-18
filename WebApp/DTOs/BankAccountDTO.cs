namespace WebApp.DTOs;
using Data.Models;
public class BankAccountDto
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto Owner { get; set; }
    public bool HasDebitCard { get; set; }
    public BankAccountType AccountType { get; set; }
    public ICollection<TransactionDto> FromTransactions { get; set; }
    public ICollection<TransactionDto> ToTransactions { get; set; }
}

public class CreateBankAccountDto
{
    public int UserId { get; set; }
    public bool HasDebitCard { get; set; }
    public BankAccountType AccountType { get; set; }
}
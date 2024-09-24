using System.ComponentModel.DataAnnotations;
using Data.Helpers;

namespace Data.Models;

public class BankAccount
{
    public int Id { get; init; }
    public int UserId { get; set; }
    
    [MaxLength(50)]
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool HasDebitCard { get; set; }
    public BankAccountType? Type { get; set; }
    public string BankAccountTypeName => BankAccountTypeHelper.GetStringFromBankAccountType(Type);

    public User? Owner { get; init; }
    public ICollection<Transaction>? FromTransactions { get; set; }
    public ICollection<Transaction>? ToTransactions { get; set; }
}

public enum BankAccountType
{
    Checking = 0,
    Savings = 1,
    CertificateOfDeposit = 2,
    Investing = 3
}

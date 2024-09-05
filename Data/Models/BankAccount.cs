using System.ComponentModel.DataAnnotations;

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
    public string BankAccountTypeName =>
        Type switch
        {
            BankAccountType.Checking => "Checking",
            BankAccountType.Savings => "Savings",
            BankAccountType.CertificateOfDeposit => "Certificate of Deposit",
            BankAccountType.Investing => "Investing",
            _ => throw new ArgumentOutOfRangeException("No such bank account type")
        };

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

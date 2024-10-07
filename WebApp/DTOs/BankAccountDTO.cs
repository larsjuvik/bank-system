using System.Text;

namespace WebApp.DTOs;
using Data.Models;
public class BankAccountDto
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto Owner { get; set; }
    public string? CoverArt { get; set; }
    public bool HasDebitCard { get; set; }
    public BankAccountType Type { get; set; }
    public ICollection<TransactionDto> FromTransactions { get; set; }
    public ICollection<TransactionDto> ToTransactions { get; set; }

    public string AccountNumberFormatted => FormatAccountNumber(AccountNumber);
    
    static string FormatAccountNumber(string accountNumber)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < accountNumber.Length; i++)
        {
            var shouldAddSpace = i > 0 && i % 4 == 0;
            if (shouldAddSpace)
            {
                builder.Append(' ');
            }
            builder.Append(accountNumber[i]);
        }
        return builder.ToString();
    }
}

public class CreateBankAccountDto
{
    public bool HasDebitCard { get; set; }
    public BankAccountType AccountType { get; set; }
}
namespace WebApp.DTOs;
using Data.Models;
public class BankAccountDTO
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDTO Owner { get; set; }
    public bool HasDebitCard { get; set; }
    public BankAccountType AccountType { get; set; }
    public ICollection<TransactionDTO> FromTransactions { get; set; }
    public ICollection<TransactionDTO> ToTransactions { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class ConnectCreditCardToBankAccountDto
{
    [MaxLength(20)]
    public string? Name { get; set; } = default;

    [Required]
    public BankAccountDto? ConnectedBankAccountDto { get; set; }
}
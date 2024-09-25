using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class ConnectCardToBankAccountDto
{
    [MaxLength(20)]
    public string? Name { get; set; } = default;

    [Required]
    public string ConnectedBankAccountNumber { get; set; }
}
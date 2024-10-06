using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class ConnectCardToBankAccountDto
{
    [Required]
    public string ConnectedBankAccountNumber { get; set; }
}
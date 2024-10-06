using System.ComponentModel.DataAnnotations;
using WebApp.Components.CustomComponents;

namespace WebApp.DTOs;

public class ConnectCardToBankAccountDto
{
    [Required]
    public string ConnectedBankAccountNumber { get; set; }
    public string? CoverArt { get; set; } = DebitCard.CoverArtOptions.Keys.Skip(1).FirstOrDefault();
}
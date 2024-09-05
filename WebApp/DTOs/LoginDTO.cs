namespace WebApp.DTOs;
using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string? Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    [MaxLength(100)]
    public string? Password { get; set; }
}
using System.ComponentModel.DataAnnotations;

public class RegisterDTO
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

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    [MaxLength(100)]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
    public string? RepeatPassword { get; set; }
}
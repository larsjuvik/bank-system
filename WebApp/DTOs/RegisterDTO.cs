namespace WebApp.DTOs;
using System.ComponentModel.DataAnnotations;
using BankSystem.WebApp.DTOs.DataAnnotations;
using Data.Models;

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
    [Display(Name = "Repeat Password")]
    [MinLength(8)]
    [MaxLength(100)]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
    public string? RepeatPassword { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    [MinimumAge(18, ErrorMessage = "You must be at least 18 years old.")]
    public DateTime? BirthDate { get; set; }

    [Required]
    [MinLength(1)]
    public string? Name { get; set; }
}
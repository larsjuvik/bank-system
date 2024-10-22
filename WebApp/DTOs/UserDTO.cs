namespace WebApp.DTOs;

public class UserDto
{
    public required string Username { get; init; }
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    public ICollection<BankAccountDto>? BankAccounts { get; init; }
    public ICollection<UserLoginDto>? UserLogins { get; init; }
}
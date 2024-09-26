namespace WebApp.DTOs;
public class UserDto
{
    public string Username { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<BankAccountDto> BankAccounts { get; set; }
    public ICollection<UserLoginDto> UserLogins { get; set; }
}
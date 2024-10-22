namespace WebApp.DTOs;

public class UserLoginDto
{
    public required DateTime LoginDateTime { get; init; }
    public required UserDto User { get; init; }
}
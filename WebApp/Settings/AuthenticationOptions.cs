namespace WebApp.Settings;

public class AuthenticationOptions
{
    public const string SectionKey = "Authentication";
    
    public int CookieExpirationMinutes { get; set; } = 20;
    public string CookieName { get; init; } = "Authentication";
}
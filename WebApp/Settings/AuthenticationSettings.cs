namespace WebApp.Settings;

public class AuthenticationSettings
{
    public const string SectionKey = "Authentication";
    
    public int CookieExpirationMinutes { get; set; } = 20;
    public string CookieName { get; init; } = "Authentication";
}
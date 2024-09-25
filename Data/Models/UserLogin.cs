namespace Data.Models;

public class UserLogin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime LoginDateTime { get; set; }
    
    public User User { get; set; }
}
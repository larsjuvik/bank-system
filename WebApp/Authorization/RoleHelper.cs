namespace WebApp.Authorization;

public static class RoleHelper
{
    public enum Role
    {
        Admin,
        User
    }

    public static string GetRoleName(Role role)
    {
        return role switch
        {
            Role.Admin => "Admin",
            Role.User => "User",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}
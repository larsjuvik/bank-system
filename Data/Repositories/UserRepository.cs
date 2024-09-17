using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(BankContext context)
{
    public async Task SaveUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public IQueryable<User> GetAllUsersWithBankAccountsAsQueryableAsync()
    {
        return context.Users
            .Include(u => u.BankAccounts);
    }

    public async Task CreateUserAsync(string username, string password, string name, DateTime birthDate, bool isAdmin = false)
    {
        User.CreateSaltAndHash(password, out var salt, out var passwordHash);

        var user = new User
        {
            Username = username,
            Name = name,
            Salt = salt,
            PasswordHash = passwordHash,
            BirthDate = birthDate,
            IsAdmin = isAdmin
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<bool> VerifyUserCredentialsAsync(string username, string password)
    {
        var user = await context.Users.FirstAsync(u => u.Username == username);
        var verified = User.VerifyPassword(password, user.PasswordHash, user.Salt);
        return verified;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await context.Users
            .AnyAsync(u => u.Username == username);
    }

    public async Task<User> GetIdByUsernameAsync(string username)
    {
        return await context.Users.FirstAsync(u => u.Username == username);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await context.Users
            .FirstAsync(u => u.Username == username);
    }
}
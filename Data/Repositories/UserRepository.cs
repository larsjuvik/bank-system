using BankSystem.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository
{
    private readonly BankContext _context;

    public UserRepository(BankContext context)
    {
        _context = context;
    }

    public async Task SaveUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsersWithBankAccountsAsync()
    {
        return await _context.Users
            .Include(u => u.BankAccounts)
            .ToListAsync();
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
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> VerifyUserCredentialsAsync(string username, string password)
    {
        var user = await _context.Users.FirstAsync(u => u.Username == username);
        var verified = Data.Models.User.VerifyPassword(password, user.PasswordHash, user.Salt);
        return verified;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username);
    }

    public async Task<User> GetIdByUsernameAsync(string username)
    {
        return await _context.Users.FirstAsync(u => u.Username == username);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstAsync(u => u.Username == username);
    }
}
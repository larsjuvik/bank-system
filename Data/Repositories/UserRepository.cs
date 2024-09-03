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
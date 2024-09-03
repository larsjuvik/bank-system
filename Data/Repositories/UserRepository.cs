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

    public async Task<User> GetIdByUsernameAsync(string username)
    {
        return await _context.Users.FirstAsync(u => u.Username == username);
    }
}
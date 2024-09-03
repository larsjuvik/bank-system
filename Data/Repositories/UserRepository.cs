using BankSystem.Data;
using Data.Models;

namespace Data.Repositories;

public class UserRepository
{
    private readonly BankContext _context;

    public UserRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
using BankSystem.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BankAccountRepository
{
    private readonly BankContext _context;

    public BankAccountRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<List<BankAccount>> GetAllBankAccountsByUserIdAsync(int id)
    {
        return await _context.BankAccounts
            .Include(b => b.Owner)
            .Where(b => b.Owner.Id == id).ToListAsync();
    }
}
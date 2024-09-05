using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BankAccountRepository(BankContext context)
{
    public async Task<List<BankAccount>> GetAllBankAccountsByUserIdAsync(int id)
    {
        return await context.BankAccounts
            .Include(b => b.Owner)
            .Where(b => b.Owner.Id == id).ToListAsync();
    }
}
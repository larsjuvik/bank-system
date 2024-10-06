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

    public async Task AddBankAccount(int userId, BankAccountType modelAccountType, bool modelHasDebitCard)
    {
        var model = new BankAccount()
        {
            UserId = userId,
            AccountNumber = BankContext.GetRandomAccountNumber(),  // TODO: ensure this is not taken
            CreatedDate = DateTime.UtcNow,
            Balance = 0,
            HasDebitCard = modelHasDebitCard,
            Type = modelAccountType
        };
        await context.BankAccounts.AddAsync(model);
        await context.SaveChangesAsync();
    }
}
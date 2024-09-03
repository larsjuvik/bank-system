using BankSystem.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TransactionRepository
{
    private readonly BankContext _context;

    public TransactionRepository(BankContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get transactions by user id, where the user is the sender of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A list of transactions</returns>
    public async Task<List<Transaction>> GetFromTransactionByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.From)
            .Where(t => t.From.Id == id)
            .ToListAsync();
    }
}
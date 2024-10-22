using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TransactionRepository(BankContext context)
{
    /// <summary>
    /// Get transactions by user id, where the user is the sender or receiver of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A queryable transactions object</returns>
    public IQueryable<Transaction> GetAllTransactionsByUserIdAsync(int id)
    {
        return context.Transactions
            .Include(t => t.From)
            .Include(t => t.To)
            .Where(t => t.To != null && t.To.Id == id);
    }

    /// <summary>
    /// Get transactions by user id, where the user is the sender of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A list of transactions</returns>
    public async Task<List<Transaction>> GetFromTransactionsByUserIdAsync(int id)
    {
        return await context.Transactions
            .Include(t => t.From)
            .Include(t => t.To)
            .Where(t => t.From.Id == id)
            .ToListAsync();
    }

    /// <summary>
    /// Get transactions by user id, where the user is the receiver of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A list of transactions</returns>
    public async Task<List<Transaction>> GetToTransactionsByUserIdAsync(int id)
    {
        return await context.Transactions
            .Include(t => t.From)
            .Include(t => t.To)
            .Where(t => t.To.Id == id)
            .ToListAsync();
    }
}
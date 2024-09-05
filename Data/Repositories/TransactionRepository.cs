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
    /// Get transactions by user id, where the user is the sender or receiver of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A list of transactions</returns>
    public async Task<List<Transaction>> GetAllTransactionsByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.From)
            .Include(t => t.To)
            .Where(t => t.To.Id == id)
            .ToListAsync();
    }

    /// <summary>
    /// Get transactions by user id, where the user is the sender of the transaction
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>A list of transactions</returns>
    public async Task<List<Transaction>> GetFromTransactionsByIdAsync(int id)
    {
        return await _context.Transactions
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
    public async Task<List<Transaction>> GetToTransactionsByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.From)
            .Include(t => t.To)
            .Where(t => t.To.Id == id)
            .ToListAsync();
    }
}
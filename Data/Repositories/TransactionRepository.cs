using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TransactionRepository(BankContext context)
{
    /// <summary>
    /// Get transactions by user id, where the user is the sender or receiver of the transaction
    /// </summary>
    /// <param name="userId">The id of the user</param>
    /// <returns>A queryable transactions object</returns>
    public IQueryable<Transaction> GetAllTransactionsByUserIdAsync(int userId)
    {
        return context.Transactions
            .Include(t => t.From)
            .ThenInclude(b=>b.Owner)
            .Include(t => t.To)
            .ThenInclude(b=>b.Owner)
            .Where(t => t.To != null && t.From != null && (t.To.Owner.Id == userId || t.From.Owner.Id == userId));
    }
}
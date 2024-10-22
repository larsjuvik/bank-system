using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class TransactionService(TransactionRepository transactionRepository, IMapper mapper)
{
    public IQueryable<TransactionDto> GetAllTransactionsByUserIdAsync(int id)
    {
        var model = transactionRepository.GetAllTransactionsByUserIdAsync(id);
        return mapper.ProjectTo<TransactionDto>(model);
    }
}
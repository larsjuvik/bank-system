using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class TransactionService(TransactionRepository transactionRepository, IMapper mapper)
{
    public async Task<List<TransactionDto>> GetAllTransactionsByUserIdAsync(int id)
    {
        var model = await transactionRepository.GetAllTransactionsByUserIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }

    public async Task<List<TransactionDto>> GetFromTransactionsByUserIdAsync(int id)
    {
        var model = await transactionRepository.GetFromTransactionsByUserIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }

    public async Task<List<TransactionDto>> GetToTransactionsByUserIdAsync(int id)
    {
        var model = await transactionRepository.GetToTransactionsByUserIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }
}
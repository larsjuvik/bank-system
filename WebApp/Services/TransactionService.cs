using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class TransactionService(TransactionRepository transactionRepository, IMapper mapper)
{
    public async Task<List<TransactionDto>> GetAllTransactionsByIdAsync(int id)
    {
        var model = await transactionRepository.GetAllTransactionsByIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }

    public async Task<List<TransactionDto>> GetFromTransactionsByIdAsync(int id)
    {
        var model = await transactionRepository.GetFromTransactionsByIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }

    public async Task<List<TransactionDto>> GetToTransactionsByIdAsync(int id)
    {
        var model = await transactionRepository.GetToTransactionsByIdAsync(id);
        return mapper.Map<List<TransactionDto>>(model);
    }
}
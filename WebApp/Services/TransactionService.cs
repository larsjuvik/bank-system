using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class TransactionService
{
    private readonly TransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(TransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<List<TransactionDTO>> GetAllTransactionsByIdAsync(int id)
    {
        var model = await _transactionRepository.GetAllTransactionsByIdAsync(id);
        return _mapper.Map<List<TransactionDTO>>(model);
    }

    public async Task<List<TransactionDTO>> GetFromTransactionsByIdAsync(int id)
    {
        var model = await _transactionRepository.GetFromTransactionsByIdAsync(id);
        return _mapper.Map<List<TransactionDTO>>(model);
    }

    public async Task<List<TransactionDTO>> GetToTransactionsByIdAsync(int id)
    {
        var model = await _transactionRepository.GetToTransactionsByIdAsync(id);
        return _mapper.Map<List<TransactionDTO>>(model);
    }
}
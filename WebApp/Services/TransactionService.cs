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

    public async Task<List<TransactionDTO>> GetFromTransactionByIdAsync(int id)
    {
        var model = await _transactionRepository.GetFromTransactionByIdAsync(id);
        return _mapper.Map<List<TransactionDTO>>(model);
    }
}
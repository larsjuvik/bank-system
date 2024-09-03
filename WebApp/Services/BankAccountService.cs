using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class BankAccountService
{
    private readonly BankAccountRepository _bankAccountRepository;
    private readonly IMapper _mapper;

    public BankAccountService(BankAccountRepository bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<List<BankAccountDTO>> GetAllBankAccountsByUserIdAsync(int id)
    {
        var model = await _bankAccountRepository.GetAllBankAccountsByUserIdAsync(id);
        return _mapper.Map<List<BankAccountDTO>>(model);
    }
}
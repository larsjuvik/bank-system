using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class BankAccountService(BankAccountRepository bankAccountRepository, IMapper mapper)
{
    public async Task ConnectCardToBankAccount(ConnectCardToBankAccountDto connectCardToBankAccountDto)
    {
        // TODO make sure no cards are connected to this account number
        
        // TODO make sure this is user's own bank account
        throw new NotImplementedException();
    }

    public async Task<List<BankAccountDto>> GetAllBankAccountsByUserIdAsync(int id)
    {
        var model = await bankAccountRepository.GetAllBankAccountsByUserIdAsync(id);
        return mapper.Map<List<BankAccountDto>>(model);
    }
}
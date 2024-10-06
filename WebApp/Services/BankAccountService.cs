using Data.Repositories;
using AutoMapper;
using Data.Models;
using WebApp.DTOs;

namespace WebApp.Services;

public class BankAccountService(BankAccountRepository bankAccountRepository, UserRepository userRepository, IMapper mapper)
{
    /// <summary>
    /// Tries to connect card to a bank account.
    ///
    /// Returns a non-null client-friendly error string if something went wrong.
    /// </summary>
    public async Task<string?> AddCardToBankAccount(string username, ConnectCardToBankAccountDto connectCardToBankAccountDto)
    {
        // Check that the bank account owner is not null
        var bankAccount = await bankAccountRepository.GetBankAccountByAccountNumberAsync(connectCardToBankAccountDto.ConnectedBankAccountNumber);
        if (bankAccount == null)
        {
            return "Bank account not found";
        }
        if (bankAccount.Owner?.Id == null)
        {
            return "No owner registered on requested bank account";
        }

        // Get user id of whom is trying to add a card
        var userId = await userRepository.GetIdByUsernameAsync(username);
        if (userId != bankAccount.Owner.Id)
        {
            return "You are not the owner of this bank account";
        }

        // Make sure no cards are already connected to this account number
        var cardAlreadyConnected = bankAccount.HasDebitCard;
        if (cardAlreadyConnected)
        {
            return "There is already a card connected to the bank account.";
        }
        
        // Add card to bank account
        await bankAccountRepository.AddCardToBankAccountAsync(bankAccount.Id, connectCardToBankAccountDto.CoverArt);
        return null;
    }

    public async Task<List<BankAccountDto>> GetAllBankAccountsByUserIdAsync(int id)
    {
        var model = await bankAccountRepository.GetAllBankAccountsByUserIdAsync(id);
        return mapper.Map<List<BankAccountDto>>(model);
    }

    public async Task AddBankAccount(string username, CreateBankAccountDto model)
    {
        var userId = await userRepository.GetIdByUsernameAsync(username);
        await bankAccountRepository.AddBankAccount(userId, model.AccountType, model.HasDebitCard);
    }
}
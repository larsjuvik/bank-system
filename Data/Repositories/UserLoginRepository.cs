using Data.Models;

namespace Data.Repositories;

public class UserLoginRepository(BankContext context)
{
    public async Task AddUserLogin(UserLogin userLogin)
    {
        await context.UserLogins.AddAsync(userLogin);
        await context.SaveChangesAsync();
    }
}
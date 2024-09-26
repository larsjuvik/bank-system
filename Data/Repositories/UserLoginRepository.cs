using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserLoginRepository(BankContext context)
{
    public async Task AddUserLogin(UserLogin userLogin)
    {
        await context.UserLogins.AddAsync(userLogin);
        await context.SaveChangesAsync();
    }

    public IQueryable<UserLogin> GetUserLoginsWithUserAsQueryable()
    {
        return context.UserLogins
            .Include(ul => ul.User)
            .AsQueryable();
    }
}
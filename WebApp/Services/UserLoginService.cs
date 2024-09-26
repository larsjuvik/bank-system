using Data.Models;
using Data.Repositories;

namespace WebApp.Services;

public class UserLoginService(UserLoginRepository userLoginRepository, UserRepository userRepository)
{
    public async Task AddUserLoginByUsername(string username)
    {
        var model = new UserLogin()
        {
            LoginDateTime = DateTime.Now,
            UserId = await userRepository.GetIdByUsernameAsync(username)
        };
        await userLoginRepository.AddUserLogin(model);
    }
}
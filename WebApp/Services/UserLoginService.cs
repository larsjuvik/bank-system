using AutoMapper;
using Data.Models;
using Data.Repositories;
using WebApp.DTOs;

namespace WebApp.Services;

public class UserLoginService(UserLoginRepository userLoginRepository, UserRepository userRepository, IMapper mapper)
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
    
    public IQueryable<UserLoginDto> GetUserLoginsWithUserAsQueryable()
    {
        var queryable = userLoginRepository.GetUserLoginsWithUserAsQueryable();
        return mapper.ProjectTo<UserLoginDto>(queryable);
    }
}
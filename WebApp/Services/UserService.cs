using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class UserService(UserRepository userRepository, IMapper mapper)
{
    public async Task<EditUserDto> GetEditUserDtoFromUsername(string username)
    {
        var model = await userRepository.GetUserByUsernameAsync(username);
        return mapper.Map<EditUserDto>(model);
    }

    public async Task SaveUserAsync(EditUserDto user)
    {
        var model = await userRepository.GetUserByUsernameAsync(user.Username);
        mapper.Map(user, model);
        await userRepository.SaveUserAsync(model);
    }

    public IQueryable<UserDto> GetAllUsersWithBankAccountsAsQueryableAsync()
    {
        var models = userRepository.GetAllUsersWithBankAccountsAsQueryableAsync();
        return mapper.ProjectTo<UserDto>(models);
    }

    public async Task CreateUserAsync(RegisterDto registerUser, bool isAdmin = false)
    {
        await userRepository.CreateUserAsync(registerUser.Username, registerUser.Password, registerUser.Name, registerUser.BirthDate, isAdmin);
    }

    public async Task<bool> VerifyUserCredentialsAsync(string username, string password)
    {
        return await userRepository.VerifyUserCredentialsAsync(username, password);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await userRepository.UserExistsAsync(username);
    }

    public async Task<bool> IsAdminAsync(string username)
    {
        var model = await userRepository.GetUserByUsernameAsync(username);
        return model.IsAdmin;
    }

    public async Task<int> GetIdByUsernameAsync(string username)
    {
        var model = await userRepository.GetIdByUsernameAsync(username);
        return model.Id;
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
        var model = await userRepository.GetUserByUsernameAsync(username);
        return mapper.Map<UserDto>(model);
    }
}
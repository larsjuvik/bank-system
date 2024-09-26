using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

namespace WebApp.Services;

public class UserService(UserRepository userRepository, IMapper mapper)
{
    public async Task<EditUserDto> GetEditUserDtoFromUsername(string username)
    {
        var model = await userRepository.GetUserWithLoginsByUsernameAsync(username);
        return mapper.Map<EditUserDto>(model);
    }

    public async Task SaveUserAsync(EditUserDto user)
    {
        var model = await userRepository.GetUserWithLoginsByUsernameAsync(user.Username);
        mapper.Map(user, model);
        await userRepository.SaveUserAsync(model);
    }

    public IQueryable<UserDto> GetAllUsersWithBankAccountsAsQueryableAsync()
    {
        var models = userRepository.GetAllUsersWithBankAccountsAsQueryableAsync();
        return mapper.ProjectTo<UserDto>(models);
    }

    public async Task CreateUserAsync(RegisterDto registerUser)
    {
        if (registerUser.BirthDate == null)
        {
            throw new NullReferenceException("Please provide a non-null birth date.");
        }

        await userRepository.CreateUserAsync(registerUser.Username, registerUser.Password, registerUser.Name, registerUser.BirthDate.Value, registerUser.IsAdmin);
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
        var model = await userRepository.GetUserWithLoginsByUsernameAsync(username);
        return model.IsAdmin;
    }

    public async Task<int> GetIdByUsernameAsync(string username)
    {
        return await userRepository.GetIdByUsernameAsync(username);
    }

    public async Task<UserDto> GetUserWithLoginsByUsernameAsync(string username)
    {
        var model = await userRepository.GetUserWithLoginsByUsernameAsync(username);
        return mapper.Map<UserDto>(model);
    }
}
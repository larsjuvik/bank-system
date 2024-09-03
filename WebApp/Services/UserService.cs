using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task CreateUserAsync(RegisterDTO registerUser, bool isAdmin = false)
    {
        await _userRepository.CreateUserAsync(registerUser.Username, registerUser.Password, registerUser.Name, registerUser.BirthDate, isAdmin);
    }

    public async Task<bool> VerifyUserCredentialsAsync(string username, string password)
    {
        return await _userRepository.VerifyUserCredentialsAsync(username, password);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _userRepository.UserExistsAsync(username);
    }

    public async Task<bool> IsAdminAsync(string username)
    {
        var model = await _userRepository.GetUserByUsernameAsync(username);
        return model.IsAdmin;
    }

    public async Task<int> GetIdByUsernameAsync(string username)
    {
        var model = await _userRepository.GetIdByUsernameAsync(username);
        return model.Id;
    }

    public async Task<UserDTO> GetUserByUsernameAsync(string username)
    {
        var model = await _userRepository.GetUserByUsernameAsync(username);
        return _mapper.Map<UserDTO>(model);
    }
}
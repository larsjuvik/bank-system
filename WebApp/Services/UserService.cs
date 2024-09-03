using Data.Repositories;
using AutoMapper;
using WebApp.DTOs;

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

    public async Task<int> GetIdByUsernameAsync(string username)
    {
        var model = await _userRepository.GetIdByUsernameAsync(username);
        return model.Id;
    }

    public async Task<UserDTO> GetUserByUsernameAsync(string username)
    {
        var model = await _userRepository.GetIdByUsernameAsync(username);
        return _mapper.Map<UserDTO>(model);
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    [HttpPost("[action]")]
    public async Task Login([FromBody] LoginDto user)
    {
        Console.WriteLine($"Trying to log in: {user.Username}");
    }
}
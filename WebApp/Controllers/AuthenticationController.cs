using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorization;
using WebApp.DTOs;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly UserService _userService;

    public AuthenticationController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDTO, string returnUrl = "/")
    {
        var errorMessage = string.Empty;
        
        // Check if the user exists
        var userExists = await _userService.UserExistsAsync(loginDTO.Username);
        if (!userExists)
        {
            errorMessage = "Invalid credentials";
            return BadRequest(errorMessage);
        }

        // Verify the password
        var credentialsVerified = await _userService.VerifyUserCredentialsAsync(loginDTO.Username, loginDTO.Password);
        if (!credentialsVerified)
        {
            errorMessage = "Invalid credentials";
            return BadRequest(errorMessage);
        }

        // Create a new identity for a user
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Name, loginDTO.Username),
            new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.User))
        ], CookieAuthenticationDefaults.AuthenticationScheme);

        // Check if the user is an admin
        if (await _userService.IsAdminAsync(loginDTO.Username))
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.Admin)));
        }

        // Create a new ClaimsPrincipal with the identity
        var user = new ClaimsPrincipal(identity);

        // Sign in the user
        if (HttpContext == null)
        {
            errorMessage = "An unexpected error occured, please try again";
            return BadRequest(errorMessage);
        }
        await HttpContext.SignInAsync(user);

        return LocalRedirect(returnUrl);
    }
}
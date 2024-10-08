using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authorization;
using WebApp.DTOs;
using WebApp.Services;
using WebApp.Settings;
using AuthenticationOptions = WebApp.Settings.AuthenticationOptions;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class AuthenticationController(UserService userService, AuthenticationOptions authOptions) : Controller
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // Null-checks
        if (loginDto.Username == null || loginDto.Password == null)
        {
            return BadRequest("Please provide non-null username and password");
        }
        
        var validationContext = new ValidationContext(loginDto);
        var validObject = Validator.TryValidateObject(loginDto, validationContext, null, true);
        if (!validObject)
        {
            return BadRequest("Validation failed");
        }

        // Check if the user exists
        var userExists = await userService.UserExistsAsync(loginDto.Username);
        if (!userExists)
        {
            return BadRequest("Invalid credentials");
        }

        // Verify the password
        var credentialsVerified = await userService.VerifyUserCredentialsAsync(loginDto.Username, loginDto.Password);
        if (!credentialsVerified)
        {
            return BadRequest("Invalid credentials");
        }

        // Create identity for user
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Name, loginDto.Username),
            new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.User))
        ], CookieAuthenticationDefaults.AuthenticationScheme);

        // Check if the user is an admin
        if (await userService.IsAdminAsync(loginDto.Username))
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.Admin)));
        }

        // Create a new ClaimsPrincipal with the identity
        var user = new ClaimsPrincipal(identity);

        // Sign in the user
        await HttpContext.SignInAsync(user);
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        // Check if the user exists
        var userExists = await userService.UserExistsAsync(registerDto.Username);
        if (userExists)
        {
            return BadRequest("Username already exists");
        }
        
        var validationContext = new ValidationContext(registerDto);
        var validObject = Validator.TryValidateObject(registerDto, validationContext, null, true);
        if (!validObject)
        {
            return BadRequest("Validation failed");
        }

        // Save
        await userService.CreateUserAsync(registerDto);

        // Create a new ClaimsIdentity with the user's name and role
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Name, registerDto.Username),
            new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.User))
        ], CookieAuthenticationDefaults.AuthenticationScheme);

        // If admin, give admin rights
        if (registerDto.IsAdmin)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, RoleHelper.GetRoleName(RoleHelper.Role.Admin)));
        }

        // Create a new ClaimsPrincipal with the identity
        var user = new ClaimsPrincipal(identity);

        // Sign in the user
        await HttpContext.SignInAsync(user);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        // Check that a user is logged in
        if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
        {
            return BadRequest("Not logged in");
        }
        
        // Sign out the user
        await HttpContext.SignOutAsync();
        
        // Delete auth cookie
        HttpContext.Response.Cookies.Delete(authOptions.CookieName);
        return Ok();
    }
}